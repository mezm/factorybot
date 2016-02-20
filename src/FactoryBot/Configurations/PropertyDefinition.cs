using System.Reflection;

using FactoryBot.Generators;

namespace FactoryBot.Configurations
{
    internal class PropertyDefinition
    {
        public PropertyDefinition(PropertyInfo property, IGenerator generator)
        {
            Check.NotNull(property, nameof(property));
            Check.NotNull(generator, nameof(generator));

            Property = property;
            Generator = generator;
        }

        public PropertyInfo Property { get; }

        public IGenerator Generator { get; }

        public void Apply(object obj)
        {
            Check.NotNull(obj, nameof(obj));

            Property.SetValue(obj, Generator.Next());
        }
    }
}