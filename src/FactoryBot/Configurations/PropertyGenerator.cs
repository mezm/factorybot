using System.Reflection;

using FactoryBot.Generators;

namespace FactoryBot.Configurations
{
    internal class PropertyGenerator
    {
        public PropertyInfo Property { get; set; }

        public IGenerator Generator { get; set; }
    }
}