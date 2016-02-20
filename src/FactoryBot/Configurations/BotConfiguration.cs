using System;
using System.Collections.Generic;
using System.Linq;
using FactoryBot.Extensions;
using FactoryBot.Generators;

namespace FactoryBot.Configurations
{
    internal class BotConfiguration
    {
        public BotConfiguration(Type constructingType, ConstructorGenerator constructor)
        {
            Check.NotNull(constructingType, nameof(constructingType));
            Check.NotNull(constructor, nameof(constructor));

            ConstructingType = constructingType;
            Constructor = constructor;
        }

        public Type ConstructingType { get; }

        public ConstructorGenerator Constructor { get; }

        public List<PropertyGenerator> Properties { get; } = new List<PropertyGenerator>();

        public object CreateNewObject()
        {
            return Create(Constructor);
        }

        public object CreateNewObjectWithModification(ConstructorGenerator modification)
        {
            Check.NotNull(modification, nameof(modification));

            if (Constructor.Constructor != modification.Constructor)
            {
                throw new InvalidOperationException(
                    $"Constructors mismatch. Origin: {Constructor.Constructor}, modification: {modification.Constructor}");
            }

            var args = new IGenerator[Constructor.Arguments.Count];
            for (var i = 0; i < args.Length; i++)
            {
                var modifiedArg = modification.Arguments[i];
                args[i] = modifiedArg is KeepGenerator ? Constructor.Arguments[i] : modifiedArg;
            }

            var patchedConstructor = new ConstructorGenerator(Constructor.Constructor, args);
            return Create(patchedConstructor);
        }

        public Type[] GetNestedDependencies()
        {
            return Constructor.Arguments.Where(x => x.IsUsingDecorator())
                .Concat(Properties.Where(x => x.Generator.IsUsingDecorator()).Select(x => x.Generator))
                .Select(x => x.GetDependencyType())
                .Distinct()
                .ToArray();
        }

        private object Create(ConstructorGenerator constructor)
        {
            var obj = constructor.Create();
            foreach (var property in Properties)
            {
                property.Apply(obj);
            }

            return obj;
        }
    }
}