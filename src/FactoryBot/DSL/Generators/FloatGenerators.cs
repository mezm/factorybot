using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Float generators
    /// </summary>
    public class FloatGenerators : IPrimitiveGenerators<float, float>
    {
#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random float value
        /// </summary>
        /// <returns>Float value</returns>
        [Generator(typeof(FloatRandomGenerator))]
        public float Any() => default;

        /// <summary>
        /// Generates random float value in a given range
        /// </summary>
        /// <param name="from">Minimum value</param>
        /// <param name="to">Maximum value</param>
        /// <returns>Float value</returns>
        [Generator(typeof(FloatRandomGenerator))]
        public float Any(float from, float to) => default;

        /// <summary>
        /// Returns float from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Float value</returns>
        [Generator(typeof(RandomFromListGenerator<float>))]
        public float RandomFromList(IReadOnlyList<float> source) => default;

        /// <summary>
        /// Returns float from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Float value</returns>
        [Generator(typeof(RandomFromListGenerator<float>))]
        public float RandomFromList(params float[] source) => default;

        /// <summary>
        /// Returns float from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Float value</returns>
        [Generator(typeof(SequenceFromListGenerator<float>))]
        public float SequenceFromList(IReadOnlyList<float> source) => default;

        /// <summary>
        /// Returns float from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Float value</returns>
        [Generator(typeof(SequenceFromListGenerator<float>))]
        public float SequenceFromList(params float[] source) => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}