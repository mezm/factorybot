using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Integer generators
    /// </summary>
    public class IntegerGenerators : IPrimitiveGenerators<int, int>
    {
        /// <summary>
        /// Generates random integer 
        /// </summary>
        /// <returns>Integer value</returns>
        [Generator(typeof(IntegerRandomGenerator))]
        public int Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random integer in a given range
        /// </summary>
        /// <param name="from">Minimum value</param>
        /// <param name="to">Maximum value</param>
        /// <returns>Integer value</returns>
        [Generator(typeof(IntegerRandomGenerator))]
        public int Any(int from, int to) => default;

        /// <summary>
        /// Returns integer from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Integer value</returns>
        [Generator(typeof(RandomFromListGenerator<int>))]
        public int RandomFromList(IReadOnlyList<int> source) => default;

        /// <summary>
        /// Returns integer from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Integer value</returns>
        [Generator(typeof(RandomFromListGenerator<int>))]
        public int RandomFromList(params int[] source) => default;

        /// <summary>
        /// Returns integer from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Integer value</returns>
        [Generator(typeof(SequenceFromListGenerator<int>))]
        public int SequenceFromList(IReadOnlyList<int> source) => default;

        /// <summary>
        /// Returns integer from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Integer value</returns>
        [Generator(typeof(SequenceFromListGenerator<int>))]
        public int SequenceFromList(params int[] source) => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}