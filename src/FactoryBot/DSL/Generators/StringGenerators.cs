using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Strings;

namespace FactoryBot.DSL.Generators
{
    public class StringGenerators : IPrimitiveGenerators<string, int>
    {
        [Generator(typeof(StringRandomGenerator))]
        public string Any() => Returns.Type<string>();

#pragma warning disable IDE0060 // Remove unused parameter

        [Generator(typeof(StringRandomGenerator))]
        public string Any(int minLength, int maxLength) => Returns.Type<string>();

        [Generator(typeof(WordRandomGenerator))]
        public string Words() => Returns.Type<string>();

        [Generator(typeof(WordRandomGenerator))]
        public string Words(int minWords, int maxWords) => Returns.Type<string>();

        [Generator(typeof(FilePathGenerator))]
        public string Filename() => Returns.Type<string>();

        [Generator(typeof(FilePathGenerator))]
        public string Filename(string fromFolder, bool existing) => Returns.Type<string>();

        [Generator(typeof(RandomFromListGenerator<string>))]
        public string RandomFromList(IReadOnlyList<string> source) => Returns.Type<string>();

        [Generator(typeof(RandomFromListGenerator<string>))]
        public string RandomFromList(params string[] source) => Returns.Type<string>();

        [Generator(typeof(SequenceFromListGenerator<string>))]
        public string SequenceFromList(IReadOnlyList<string> source) => Returns.Type<string>();

        [Generator(typeof(SequenceFromListGenerator<string>))]
        public string SequenceFromList(params string[] source) => Returns.Type<string>();

        [Generator(typeof(RandomLineFromFileGenerator))]
        public string RandomFromFile(string filename) => Returns.Type<string>();

        [Generator(typeof(SequenceStringFromFileGenerator))]
        public string SequenceFromFile(string filename) => Returns.Type<string>();

        [Generator(typeof(GuidGenerator))]
        public string Guid() => Returns.Type<string>();

#pragma warning restore IDE0060 // Remove unused parameter
    }
}