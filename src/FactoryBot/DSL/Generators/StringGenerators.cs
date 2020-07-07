using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Strings;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// String generators
    /// </summary>
    public class StringGenerators : IPrimitiveGenerators<string, int>
    {
        /// <summary>
        /// Generates random string
        /// </summary>
        /// <returns>String value</returns>
        [Generator(typeof(StringRandomGenerator))]
        public string Any() => default!;

#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random string with a length in a given range
        /// </summary>
        /// <param name="minLength">Minimum length</param>
        /// <param name="maxLength">Maximum length</param>
        /// <returns>String value</returns>
        [Generator(typeof(StringRandomGenerator))]
        public string Any(int minLength, int maxLength) => default!;

        /// <summary>
        /// Generates random number of random words
        /// </summary>
        /// <returns>String value</returns>
        [Generator(typeof(WordRandomGenerator))]
        public string Words() => default!;

        /// <summary>
        /// Generates random words which count is restricted by given range
        /// </summary>
        /// <param name="minWords">Minimum count of words</param>
        /// <param name="maxWords">Maximum count of words</param>
        /// <returns>String value</returns>
        [Generator(typeof(WordRandomGenerator))]
        public string Words(int minWords, int maxWords) => default!;

        /// <summary>
        /// Generates random filename 
        /// </summary>
        /// <returns>String value</returns>
        [Generator(typeof(FilePathGenerator))]
        public string Filename() => default!;

        /// <summary>
        /// Generates random filename 
        /// </summary>
        /// <param name="fromFolder">Folder where file should be located</param>
        /// <param name="existing">Whether file should exist or not</param>
        /// <returns>String value</returns>
        [Generator(typeof(FilePathGenerator))]
        public string Filename(string fromFolder, bool existing) => default!;

        /// <summary>
        /// Returns string from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>String value</returns>
        [Generator(typeof(RandomFromListGenerator<string>))]
        public string RandomFromList(IReadOnlyList<string> source) => default!;

        /// <summary>
        /// Returns string from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>String value</returns>
        [Generator(typeof(RandomFromListGenerator<string>))]
        public string RandomFromList(params string[] source) => default!;

        /// <summary>
        /// Returns string from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>String value</returns>
        [Generator(typeof(SequenceFromListGenerator<string>))]
        public string SequenceFromList(IReadOnlyList<string> source) => default!;

        /// <summary>
        /// Returns string from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>String value</returns>
        [Generator(typeof(SequenceFromListGenerator<string>))]
        public string SequenceFromList(params string[] source) => default!;

        /// <summary>
        /// Returns random string line from a given file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>String value</returns>
        [Generator(typeof(RandomLineFromFileGenerator))]
        public string RandomFromFile(string filename) => default!;

        /// <summary>
        /// Returns string line from a given file in the same order they are in the file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>String value</returns>
        [Generator(typeof(SequenceStringFromFileGenerator))]
        public string SequenceFromFile(string filename) => default!;

        /// <summary>
        /// Generates random string representation of GUID
        /// </summary>
        /// <returns>String value</returns>
        [Generator(typeof(GuidGenerator))]
        public string Guid() => default!;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}