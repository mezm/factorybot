using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FactoryBot.Generators;

namespace FactoryBot.Configurations
{
    internal class BotConfiguration
    {
        public Type ConstructingType { get; set; }

        public ConstructorInfo Constructor { get; set; }

        public List<IGenerator> ConstructorArguments { get; } = new List<IGenerator>();

        public List<PropertyGenerator> Properties { get; } = new List<PropertyGenerator>();

        public object CreateNewObject()
        {
            var obj = Constructor.Invoke(ConstructorArguments.Select(x => x.Next()).ToArray());
            foreach (var property in Properties)
            {
                property.Property.SetValue(obj, property.Generator.Next());
            }

            return obj;
        }
    }
}