using System;
using FactoryBot.Generators;

namespace FactoryBot.Extensions
{
    internal static class GeneratorExtensions
    {
        public static bool IsUsingDecorator(this IGenerator generator)
        {
            var type = generator.GetType();
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (UsingGenerator<>);
        }

        public static Type GetDependencyType(this IGenerator generator) => generator.GetType().GetGenericArguments()[0];
    }
}