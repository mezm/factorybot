using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Double generators
    /// </summary>
    public class DoubleGenerators : IPrimitiveGenerators<double, double>
    {
        /// <summary>
        /// Generates random double
        /// </summary>
        /// <returns>Double value</returns>
        [Generator(typeof(DoubleRandomGenerator))]
        public double Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random double in a given range
        /// </summary>
        /// <param name="from">Minimum value</param>
        /// <param name="to">Maximum value</param>
        /// <returns>Double value</returns>
        [Generator(typeof(DoubleRandomGenerator))]
        public double Any(double from, double to) => default;

        /// <summary>
        /// Returns double from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Double value</returns>
        [Generator(typeof(RandomFromListGenerator<double>))]
        public double RandomFromList(IReadOnlyList<double> source) => default;

        /// <summary>
        /// Returns double from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Double value</returns>
        [Generator(typeof(RandomFromListGenerator<double>))]
        public double RandomFromList(params double[] source) => default;

        /// <summary>
        /// Returns double from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Double value</returns>
        [Generator(typeof(SequenceFromListGenerator<double>))]
        public double SequenceFromList(IReadOnlyList<double> source) => default;

        /// <summary>
        /// Returns double from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Double value</returns>
        [Generator(typeof(SequenceFromListGenerator<double>))]
        public double SequenceFromList(params double[] source) => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}