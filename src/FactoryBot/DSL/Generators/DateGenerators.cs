using System;
using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Dates;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// DateTime generators
    /// </summary>
    public class DateGenerators : IPrimitiveGenerators<DateTime, DateTime>
    {
        /// <summary>
        /// Generates random DateTime
        /// </summary>
        /// <returns>DateTime value</returns>
        [Generator(typeof(DateTimeRandomGenerator))]
        public DateTime Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter

        /// <summary>
        /// Generates random DateTime in a range
        /// </summary>
        /// <param name="from">Minimum value</param>
        /// <param name="to">Maximum value</param>
        /// <returns>DateTime value</returns>
        [Generator(typeof(DateTimeRandomGenerator))]
        public DateTime Any(DateTime from, DateTime to) => default;

        /// <summary>
        /// Generates random DateTime after given date
        /// </summary>
        /// <param name="from">Start date</param>
        /// <returns>DateTime value</returns>
        [Generator(typeof(DateTimeRandomGenerator))]
        public DateTime After(DateTime from) => default;

        /// <summary>
        /// Generates random DateTime after current moment in time
        /// </summary>
        /// <returns>DateTime value</returns>
        [Generator(typeof(DateTimeRandomGenerator))]
        [GeneratorParameter("from", Factory = nameof(Now))]
        public DateTime AfterNow() => default;

        /// <summary>
        /// Generates random DateTime before given date
        /// </summary>
        /// <param name="to">End date</param>
        /// <returns>DateTime value</returns>
        [Generator(typeof(DateTimeRandomGenerator))]
        public DateTime Before(DateTime to) => default;

        /// <summary>
        /// Generates random DateTime before current moment in time
        /// </summary>
        /// <returns>DateTime value</returns>
        [Generator(typeof(DateTimeRandomGenerator))]
        [GeneratorParameter("to", Factory = nameof(Now))]
        public DateTime BeforeNow() => default;

        /// <summary>
        /// Returns DateTime from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>DateTime value</returns>
        [Generator(typeof(RandomFromListGenerator<DateTime>))]
        public DateTime RandomFromList(IReadOnlyList<DateTime> source) => default;

        /// <summary>
        /// Returns DateTime from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>DateTime value</returns>
        [Generator(typeof(RandomFromListGenerator<DateTime>))]
        public DateTime RandomFromList(params DateTime[] source) => default;

        /// <summary>
        /// Returns DateTime from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>DateTime value</returns>
        [Generator(typeof(SequenceFromListGenerator<DateTime>))]
        public DateTime SequenceFromList(IReadOnlyList<DateTime> source) => default;

        /// <summary>
        /// Returns DateTime from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>DateTime value</returns>
        [Generator(typeof(SequenceFromListGenerator<DateTime>))]
        public DateTime SequenceFromList(params DateTime[] source) => default;

#pragma warning restore IDE0060 // Remove unused parameter

        private static DateTime Now() => DateTime.Now;
    }
}