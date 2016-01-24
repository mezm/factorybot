using System;
using System.Linq;

using FactoryBot.Generators;

namespace FactoryBot.DSL
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GeneratorAttribute : Attribute
    {
        public GeneratorAttribute(Type generatorType)
        {
            Check.NotNull(generatorType, nameof(generatorType));
            if (generatorType.GetInterfaces().All(x => x != typeof(IGenerator)))
            {
                throw new ArgumentException($"Type {generatorType} should implement IGenerator.");
            }

            GeneratorType = generatorType;
        }

        public Type GeneratorType { get; }

        public IGenerator CreateGenerator(params object[] parameters)
        {
            Check.NotNull(parameters, nameof(parameters));

            return CreateGenerator(GeneratorType, parameters);
        }

        public IGenerator CreateGenericGenerator(Type[] genericArguments, params object[] parameters) 
        {
            Check.NotNull(genericArguments, nameof(genericArguments));
            Check.NotNull(parameters, nameof(parameters));

            return !GeneratorType.IsGenericType
                       ? CreateGenerator(parameters)
                       : CreateGenerator(GeneratorType.MakeGenericType(genericArguments), parameters);
        }

        private IGenerator CreateGenerator(Type generatorType, object[] parameters)
        {
            var parameterTypes = parameters.Select(x => x.GetType()).ToArray();
            var constructor = generatorType.GetConstructor(parameterTypes);
            if (constructor == null)
            {
                var typesString = string.Join(", ", parameterTypes.Select(x => x.Name));
                throw new MissingMethodException($"Constructor of class {GeneratorType} with parameters {typesString}.");
            }

            return (IGenerator)constructor.Invoke(parameters);
        }
    }
}