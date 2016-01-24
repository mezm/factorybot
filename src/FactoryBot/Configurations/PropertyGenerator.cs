using System.Reflection;

using FactoryBot.Generators;

namespace FactoryBot.Configurations
{
    internal class PropertyGenerator
    {
        public PropertyGenerator(PropertyInfo property, IGenerator generator)
        {
            Property = property;
            Generator = generator;
        }

        public PropertyInfo Property { get; }

        public IGenerator Generator { get; }

        public void Apply(object obj)
        {
            Property.SetValue(obj, Generator.Next());
        }
    }
}