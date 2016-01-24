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

            GeneratorType = generatorType;
        }

        public Type GeneratorType { get; }

        public IGenerator CreateGenerator(params object[] parameters)
        {
            Check.NotNull(parameters, nameof(parameters));

            var parameterTypes = parameters.Select(x => x.GetType()).ToArray();
            var constructor = GeneratorType.GetConstructor(parameterTypes);
            if (constructor == null)
            {
                var typesString = string.Join(", ", parameterTypes.Select(x => x.Name));
                throw new MissingMethodException($"Constructor of class {GeneratorType} with parameters {typesString}.");    
            }

            return (IGenerator)constructor.Invoke(parameters);
        }
    }
}