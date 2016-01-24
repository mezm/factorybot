using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FactoryBot.Generators;

namespace FactoryBot.Configurations
{
    internal class ConstructorGenerator
    {
        public ConstructorGenerator(ConstructorInfo constructor, IReadOnlyList<IGenerator> arguments)
        {
            Check.NotNull(constructor, nameof(constructor));
            Check.NotNull(arguments, nameof(arguments));

            if (constructor.GetParameters().Length != arguments.Count)
            {
                throw new ArgumentException("Constructor arguments mismatch.");
            }

            Constructor = constructor;
            Arguments = arguments;
        }

        public ConstructorInfo Constructor { get; }

        public IReadOnlyList<IGenerator> Arguments { get; }

        public object Create()
        {
            return Constructor.Invoke(Arguments.Select(x => x.Next()).ToArray());
        }
    }
}