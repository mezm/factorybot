using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public IGenerator CreateGenerator(MethodInfo method, IDictionary<string, object> parameters)
        {
            Check.NotNull(method, nameof(method));
            Check.NotNull(parameters, nameof(parameters));

            if (!GeneratorType.IsGenericType)
            {
                return CreateGenerator(GeneratorType, parameters);
            }

            if (!method.IsGenericMethod)
            {
                throw new ArgumentException("Only generic methods are allowed for generic generators.", nameof(method));
            }

            // TODO: check if generic arguments of both generator and method match 

            return CreateGenerator(GeneratorType.MakeGenericType(method.GetGenericArguments()), parameters); 
        }
        
        private IGenerator CreateGenerator(Type generatorType, IDictionary<string, object> parameters)
        {
            var constructor = generatorType.GetConstructors().FirstOrDefault(x => IsSuitableConstructor(x, parameters.Keys));
            if (constructor == null)
            {
                var typesString = string.Join(", ", parameters.Keys);
                throw new MissingMethodException($"No constructor of class {GeneratorType} with parameters {typesString} has been found.");
            }

            var constructorParameters = constructor.GetParameters().Select(x => ChooseParameter(x, parameters)).ToArray();
            return (IGenerator)constructor.Invoke(constructorParameters);
        }

        private static object ChooseParameter(ParameterInfo parameterInfo, IDictionary<string, object> parameters)
        {
            object result;
            return parameters.TryGetValue(parameterInfo.Name, out result) ? result : parameterInfo.DefaultValue;
        }

        private static bool IsSuitableConstructor(ConstructorInfo constructor, ICollection<string> parameterNames)
        {
            var parameters = constructor.GetParameters();
            return parameters.Length >= parameterNames.Count 
                && parameters.All(parameter => parameterNames.Contains(parameter.Name) || parameter.HasDefaultValue);
        }
    }
}