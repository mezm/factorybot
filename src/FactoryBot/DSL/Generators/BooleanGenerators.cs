using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Boolean generators
    /// </summary>
    public class BooleanGenerators
    {
        /// <summary>
        /// Generates random boolean value
        /// </summary>
        /// <returns>Boolean value</returns>
        [Generator(typeof(BooleanRandomGenerator))]
        public bool Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Returns boolean from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Boolean value</returns>
        [Generator(typeof(SequenceFromListGenerator<bool>))]
        public byte SequenceFromList(IReadOnlyList<bool> source) => default;

        /// <summary>
        /// Returns boolean from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Boolean value</returns>
        [Generator(typeof(SequenceFromListGenerator<bool>))]
        public byte SequenceFromList(params bool[] source) => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}