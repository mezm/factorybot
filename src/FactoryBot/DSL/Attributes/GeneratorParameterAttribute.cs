using System;
using System.Linq;
using System.Reflection;

namespace FactoryBot.DSL.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class GeneratorParameterAttribute : Attribute
    {
        public GeneratorParameterAttribute(string name) => Name = name;

        public string Name { get; }

        public object Value { get; set; }

        public string Factory { get; set; }

        public object GetParameterValue(MethodInfo method)
        {
            if (Factory == null) return Value;

            var factoryMethod = method.DeclaringType.GetMethod(Factory, BindingFlags.NonPublic | BindingFlags.Static);
            if (factoryMethod?.GetParameters()?.Any() ?? true)
            {
                throw new MissingMethodException($"Unable to find suitable factory method '{Factory}' in class {method.DeclaringType}. Factory method should be static and parameterless");
            }

            return factoryMethod.Invoke(null, Array.Empty<object>()); 
        }
    }
}