using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Strings;

namespace FactoryBot.DSL.Generators
{
    public class AddressGenerators
    {
        [Generator(typeof(CountryGenerator))]
        public string Country() => default;
    }
}