using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Long generators
    /// </summary>
    public class LongGenerators : IPrimitiveGenerators<long, long>
    {
#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random long
        /// </summary>
        /// <returns>Long value</returns>
        [Generator(typeof(LongRandomGenerator))]
        public long Any() => default;

        /// <summary>
        /// Generates random long in a given range
        /// </summary>
        /// <param name="from">Minimum value</param>
        /// <param name="to">Maximum value</param>
        /// <returns>Long value</returns>
        [Generator(typeof(LongRandomGenerator))]
        public long Any(long from, long to) => default;

        /// <summary>
        /// Returns long from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Long value</returns>
        [Generator(typeof(RandomFromListGenerator<long>))]
        public long RandomFromList(IReadOnlyList<long> source) => default;

        /// <summary>
        /// Returns long from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Long value</returns>
        [Generator(typeof(RandomFromListGenerator<long>))]
        public long RandomFromList(params long[] source) => default;

        /// <summary>
        /// Returns long from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Long value</returns>
        [Generator(typeof(SequenceFromListGenerator<long>))]
        public long SequenceFromList(IReadOnlyList<long> source) => default;

        /// <summary>
        /// Returns long from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Long value</returns>
        [Generator(typeof(SequenceFromListGenerator<long>))]
        public long SequenceFromList(params long[] source) => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}