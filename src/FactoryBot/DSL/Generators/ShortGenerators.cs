using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Short generates
    /// </summary>
    public class ShortGenerators : IPrimitiveGenerators<short, short>
    {
#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random short
        /// </summary>
        /// <returns>Short value</returns>
        [Generator(typeof(ShortRandomGenerator))]
        public short Any() => default;

        /// <summary>
        /// Generates random short in a given range 
        /// </summary>
        /// <param name="from">Minimum value</param>
        /// <param name="to">Maximum value</param>
        /// <returns>Short value</returns>
        [Generator(typeof(ShortRandomGenerator))]
        public short Any(short from, short to) => default;

        /// <summary>
        /// Returns short from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Short value</returns>
        [Generator(typeof(RandomFromListGenerator<short>))]
        public short RandomFromList(IReadOnlyList<short> source) => default;

        /// <summary>
        /// Returns short from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Short value</returns>
        [Generator(typeof(RandomFromListGenerator<short>))]
        public short RandomFromList(params short[] source) => default;

        /// <summary>
        /// Returns short from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Short value</returns>
        [Generator(typeof(SequenceFromListGenerator<short>))]
        public short SequenceFromList(IReadOnlyList<short> source) => default;

        /// <summary>
        /// Returns short from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Short value</returns>
        [Generator(typeof(SequenceFromListGenerator<short>))]
        public short SequenceFromList(params short[] source) => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}