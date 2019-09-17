using System;
using System.Collections.Generic;
using System.Linq;
using FactoryBot.Extensions;
using FactoryBot.Generators;

namespace FactoryBot.Configurations
{
    internal class BotConfiguration : IGenerator
    {
        public BotConfiguration(Type constructingType, ConstructorDefinition constructor)
        {
            Check.NotNull(constructingType, nameof(constructingType));
            Check.NotNull(constructor, nameof(constructor));

            ConstructingType = constructingType;
            Constructor = constructor;
        }

        public Type ConstructingType { get; }

        public ConstructorDefinition Constructor { get; }

        public List<PropertyDefinition> Properties { get; } = new List<PropertyDefinition>();

        public Action<object> BeforeBindingHook { get; set; } = x => { };

        public Action<object> AfterBindingHook { get; set; } = x => { };

        public object CreateNewObject() => Create(Constructor);

        public object CreateNewObjectWithModification(ConstructorDefinition modification)
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

            var patchedConstructor = new ConstructorDefinition(Constructor.Constructor, args);
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

        object IGenerator.Next() => CreateNewObject();

        private object Create(ConstructorDefinition constructor)
        {
            var obj = constructor.Create();

            try
            {
                BeforeBindingHook(obj);
            }
            catch (Exception ex)
            {
                throw new BuildFailedException($"Failed process BeforeBindingHook on creation of {ConstructingType}. See InnerException for more details.", ex);
            }

            foreach (var property in Properties)
            {
                property.Apply(obj);
            }

            try
            {
                AfterBindingHook(obj);
            }
            catch (Exception ex)
            {
                throw new BuildFailedException($"Failed process AfterBindingHook on creation of {ConstructingType}. See InnerException for more details.", ex);
            }

            return obj;
        }
    }
}