using System;
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

        public void Apply(object obj, bool overrideNotDefault = false)
        {
            Check.NotNull(obj, nameof(obj));

            if (!overrideNotDefault && !PropertyHasDefaultValue(obj))
            {
                return;
            }

            Property.SetValue(obj, Generator.Next());
        }

        private bool PropertyHasDefaultValue(object obj)
        {
            var value = Property.GetValue(obj);
            if (Property.PropertyType.IsValueType)
            {
                return Equals(value, Activator.CreateInstance(Property.PropertyType));
            }

            return value == null;
        }
    }
}