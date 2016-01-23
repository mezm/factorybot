using FactoryBot.Generators.Strings;

namespace FactoryBot.DSL
{
    public class StringGenerators
    {
        [Generator(typeof(StringRandomGenerator))]
        public string Any() => default(string);
    }
}