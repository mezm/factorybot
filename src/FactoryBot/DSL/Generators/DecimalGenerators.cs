using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Decimal generators
    /// </summary>
    public class DecimalGenerators : IPrimitiveGenerators<decimal, decimal>
    {
        /// <summary>
        /// Generates random decimal
        /// </summary>
        /// <returns>Decimal value</returns>
        [Generator(typeof(DecimalRandomGenerator))]
        public decimal Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random decimal in a given range
        /// </summary>
        /// <param name="from">Minimum value</param>
        /// <param name="to">Maximum value</param>
        /// <returns>Decimal value</returns>
        [Generator(typeof(DecimalRandomGenerator))]
        public decimal Any(decimal from, decimal to) => default;

        /// <summary>
        /// Returns decimal from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Decimal value</returns>
        [Generator(typeof(RandomFromListGenerator<decimal>))]
        public decimal RandomFromList(IReadOnlyList<decimal> source) => default;

        /// <summary>
        /// Returns decimal from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Decimal value</returns>
        [Generator(typeof(RandomFromListGenerator<decimal>))]
        public decimal RandomFromList(params decimal[] source) => default;

        /// <summary>
        /// Returns decimal from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Decimal value</returns>
        [Generator(typeof(SequenceFromListGenerator<decimal>))]
        public decimal SequenceFromList(IReadOnlyList<decimal> source) => default;

        /// <summary>
        /// Returns decimal from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Decimal value</returns>
        [Generator(typeof(SequenceFromListGenerator<decimal>))]
        public decimal SequenceFromList(params decimal[] source) => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}