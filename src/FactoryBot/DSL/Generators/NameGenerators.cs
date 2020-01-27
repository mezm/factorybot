using FactoryBot.DSL.Attributes;
using FactoryBot.Generators;
using FactoryBot.Generators.Strings;

namespace FactoryBot.DSL.Generators
{
    public class NameGenerators
    {
#pragma warning disable IDE0060 // Remove unused parameter

        [StringGeneratorFromResource(SourceNames.FIRST_NAMES)]
        public string FirstName() => default;

        [StringGeneratorFromResource(SourceNames.LAST_NAMES)]
        public string LastName() => default;

        [Generator(typeof(FullNameGenerator))]
        public string FullName() => default;

        [Generator(typeof(FullNameGenerator))]
        public string FullName(FullNameFormat format) => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}