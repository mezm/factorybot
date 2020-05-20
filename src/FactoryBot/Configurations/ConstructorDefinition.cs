using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FactoryBot.Generators;

namespace FactoryBot.Configurations
{
    internal class ConstructorDefinition
    {
        public ConstructorDefinition(ConstructorInfo constructor, IReadOnlyList<IGenerator> arguments)
        {
            if (constructor.GetParameters().Length != arguments.Count)
            {
                throw new ArgumentException("Constructor arguments mismatch.");
            }

            Constructor = constructor;
            Arguments = arguments;
        }

        public ConstructorInfo Constructor { get; }

        public IReadOnlyList<IGenerator> Arguments { get; }

        public object Create() => Constructor.Invoke(Arguments.Select(x => x.Next()).ToArray());
    }
}