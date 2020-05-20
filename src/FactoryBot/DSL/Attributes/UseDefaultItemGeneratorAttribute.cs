using FactoryBot.Generators;
using System;
using System.Reflection;

namespace FactoryBot.DSL.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class UseDefaultItemGeneratorAttribute : Attribute
    {
        public UseDefaultItemGeneratorAttribute(string parameterName, int genericParameterIndex = 0)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentException("String should not be null or empty.", nameof(parameterName));
            }

            ParameterName = parameterName;
            GenericParameterIndex = genericParameterIndex;
        }

        public string ParameterName { get; }

        public int GenericParameterIndex { get; }

        public IGenerator CreateGenerator(MethodInfo method)
        {
            if (!method.IsGenericMethod)
            {
                throw new ArgumentException("Only generic methods are allowed for generic generators.", nameof(method));
            }

            // TODO: check if generic arguments of both generator and method match 
            // todo: check if method have generic param, don't throw out of range exception
            var itemType = method.GetGenericArguments()[GenericParameterIndex]; 
            var generatorType = typeof(UsingGenerator<>).MakeGenericType(itemType);
            return (IGenerator)Activator.CreateInstance(generatorType);
        }
    }
}