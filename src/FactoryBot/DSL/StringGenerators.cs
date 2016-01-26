using FactoryBot.Generators.Strings;

namespace FactoryBot.DSL
{
    public class StringGenerators
    {
        [Generator(typeof(StringRandomGenerator))]
        public string Any() => default(string);

        [Generator(typeof(StringRandomGenerator))]
        public string Any(int minLength, int maxLength) => default(string);

        [Generator(typeof(WordRandomGenerator))]
        public string AnyWords() => default(string);

        [Generator(typeof(WordRandomGenerator))]
        public string AnyWords(int minCount, int maxCount) => default(string);
    }
}