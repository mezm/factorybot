using FactoryBot.Generators;
using FactoryBot.Generators.Strings;
using System.Collections.Generic;
using System.Reflection;

namespace FactoryBot.DSL.Attributes
{
    public sealed class StringGeneratorFromResourceAttribute : GeneratorAttributeBase
    {
        public StringGeneratorFromResourceAttribute(string resource) => Resource = resource;

        public string Resource { get; }

        public override IGenerator CreateGenerator(MethodInfo method, IDictionary<string, object> parameters)
        {
            return new RandomLineFromResourceGenerator(Resource);
        }
    }
}