using System;
using FactoryBot.Generators;

namespace FactoryBot.Extensions
{
    internal static class GeneratorExtensions
    {
        public static bool IsUsingDecorator(this IGenerator generator)
        {
            Check.NotNull(generator, nameof(generator));

            var type = generator.GetType();
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (GeneratorUsingDecorator<>);
        }

        public static Type GetDependencyType(this IGenerator generator)
        {
            Check.NotNull(generator, nameof(generator));

            return generator.GetType().GetGenericArguments()[0];
        }
    }
}