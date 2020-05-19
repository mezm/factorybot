using FactoryBot.Configurations;
using FactoryBot.Generators;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Dates;
using FactoryBot.Generators.Numbers;
using FactoryBot.Generators.Strings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FactoryBot.ExpressionParser
{
    internal class AutoBindingParser
    {
        private const int COLLECTION_MIN_LENGTH = 1;
        private const int COLLECTION_MAX_LENGTH = 100;

        public static Dictionary<Type, IGenerator> DefaultGenerators { get; }

        static AutoBindingParser()
        {
            DefaultGenerators = new Dictionary<Type, IGenerator>
            {
                [typeof(int)] = new IntegerRandomGenerator(),
                [typeof(long)] = new LongRandomGenerator(),
                [typeof(short)] = new ShortRandomGenerator(),
                [typeof(byte)] = new ByteRandomGenerator(),
                [typeof(double)] = new DoubleRandomGenerator(),
                [typeof(float)] = new FloatRandomGenerator(),
                [typeof(decimal)] = new DecimalRandomGenerator(),
                [typeof(string)] = new StringRandomGenerator(),
                [typeof(DateTime)] = new DateTimeRandomGenerator(),
                [typeof(bool)] = new BooleanRandomGenerator()
            };
        }

        public BotConfiguration Parse<T>()
        {
            var type = typeof(T);
            if (type.IsAbstract)
            {
                throw new BuildFailedException("Abstract types can't be auto generated");
            }

            return Parse(type, Array.Empty<Type>());
        }

        private static BotConfiguration Parse(Type type, Type[] parents)
        {
            var constructor = FindSuitableConstructor(type, parents);
            var config = new BotConfiguration(type, constructor);

            if (parents.Contains(type))
            {
                throw new CircularDependencyDetectedException();
            }

            parents = parents.Concat(new[] { type }).ToArray();

            var properties = type.GetProperties();
            foreach (var property in properties.Where(x => x.CanWrite))
            {
                var propertyType = property.PropertyType;
                var propertyGenerator = GetPropertyGenerator(propertyType, parents);
                config.Properties.Add(new PropertyDefinition(property, propertyGenerator));
            }

            return config;
        }

        private static ConstructorDefinition FindSuitableConstructor(Type type, Type[] parents)
        {
            var constructors = type.GetConstructors().OrderBy(x => x.GetParameters().Length);
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                if (parameters.Length == 0)
                {
                    return new ConstructorDefinition(constructor, Array.Empty<IGenerator>());
                }

                var generators = new List<IGenerator>();
                foreach (var param in parameters)
                {
                    try
                    {
                        var paramGenerator = GetPropertyGenerator(param.ParameterType, parents);
                        generators.Add(paramGenerator);
                    }
                    catch (UnknownTypeException)
                    {
                        break;
                    }
                }

                if (generators.Count == parameters.Length)
                {
                    return new ConstructorDefinition(constructor, generators);
                }
            }

            throw new BuildFailedException($"No suitable public constructor of type {type} found");
        }

        private static IGenerator GetPropertyGenerator(Type type, Type[] parents)
        {
            if (DefaultGenerators.TryGetValue(type, out var generator))
            {
                return generator;
            }

            if (type.IsArray)
            {
                var itemType = type.GetElementType();
                var itemGenerator = GetPropertyGenerator(itemType, parents);
                return CreateCollectionGenerator(typeof(ArrayGenerator<>), new[] { itemType }, new[] { itemGenerator });
            }

            if (type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
            {
                var keyType = type.GetGenericArguments()[0];
                var keyGenerator = GetPropertyGenerator(keyType, parents);

                var valueType = type.GetGenericArguments()[1];
                var valueGenerator = GetPropertyGenerator(valueType, parents);

                return CreateCollectionGenerator(typeof(DictionaryGenerator<,>), new[] { keyType, valueType }, new[] { keyGenerator, valueGenerator });
            }

            if (type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                var itemType = type.GetGenericArguments()[0];
                var itemGenerator = GetPropertyGenerator(itemType, parents);
                
                return CreateCollectionGenerator(typeof(ListGenerator<>), new[] { itemType }, new[] { itemGenerator });
            }

            if (!type.IsPrimitive)
            {
                return Parse(type, parents);
            }

            throw new UnknownTypeException(type);
        }

        private static IGenerator CreateCollectionGenerator(Type generatorType, Type[] itemType, IGenerator[] itemGenerator)
        {
            return (IGenerator)generatorType.MakeGenericType(itemType)
                        .GetConstructor(new[] { typeof(int), typeof(int) }.Concat(itemGenerator.Select(_ => typeof(IGenerator))).ToArray())
                        .Invoke(new object[] { COLLECTION_MIN_LENGTH, COLLECTION_MAX_LENGTH }.Concat(itemGenerator).ToArray());
        }
    }
}
