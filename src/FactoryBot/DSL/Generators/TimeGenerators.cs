using System;
using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Dates;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// TimeSpan generators
    /// </summary>
    public class TimeGenerators : IPrimitiveGenerators<TimeSpan, TimeSpan>
    {
        /// <summary>
        /// Generates random TimeSpan
        /// </summary>
        /// <returns>TimeSpan value</returns>
        [Generator(typeof(TimeSpanRandomGenerator))]
        public TimeSpan Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random TimeSpan in a given range
        /// </summary>
        /// <param name="from">Minimum value</param>
        /// <param name="to">Maximum value</param>
        /// <returns>TimeSpan value</returns>
        [Generator(typeof(TimeSpanRandomGenerator))]
        public TimeSpan Any(TimeSpan from, TimeSpan to) => default;

        /// <summary>
        /// Returns TimeSpan from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>TimeSpan value</returns>
        [Generator(typeof(RandomFromListGenerator<TimeSpan>))]
        public TimeSpan RandomFromList(IReadOnlyList<TimeSpan> source) => default;

        /// <summary>
        /// Returns TimeSpan from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>TimeSpan value</returns>
        [Generator(typeof(RandomFromListGenerator<TimeSpan>))]
        public TimeSpan RandomFromList(params TimeSpan[] source) => default;

        /// <summary>
        /// Returns TimeSpan from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>TimeSpan value</returns>
        [Generator(typeof(SequenceFromListGenerator<TimeSpan>))]
        public TimeSpan SequenceFromList(IReadOnlyList<TimeSpan> source) => default;

        /// <summary>
        /// Returns TimeSpan from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>TimeSpan value</returns>
        [Generator(typeof(SequenceFromListGenerator<TimeSpan>))]
        public TimeSpan SequenceFromList(params TimeSpan[] source) => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}