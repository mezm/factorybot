using FactoryBot.DSL.Attributes;
using FactoryBot.Generators;

namespace FactoryBot.DSL.Generators
{
    public class AddressGenerators
    {
        [StringGeneratorFromResource(SourceNames.COUNTRIES)]
        public string Country() => default;

        [StringGeneratorFromResource(SourceNames.CITIES)]
        public string City() => default;
    }
}