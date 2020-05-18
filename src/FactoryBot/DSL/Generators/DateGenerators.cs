using System;
using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Dates;

namespace FactoryBot.DSL.Generators
{
    public class DateGenerators : IPrimitiveGenerators<DateTime, DateTime>
    {
        [Generator(typeof(DateTimeRandomGenerator))]
        public DateTime Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter

        [Generator(typeof(DateTimeRandomGenerator))]
        public DateTime Any(DateTime from, DateTime to) => default;

        [Generator(typeof(RandomFromListGenerator<DateTime>))]
        public DateTime RandomFromList(IReadOnlyList<DateTime> source) => default;

        [Generator(typeof(RandomFromListGenerator<DateTime>))]
        public DateTime RandomFromList(params DateTime[] source) => default;

        [Generator(typeof(SequenceFromListGenerator<DateTime>))]
        public DateTime SequenceFromList(IReadOnlyList<DateTime> source) => default;

        [Generator(typeof(SequenceFromListGenerator<DateTime>))]
        public DateTime SequenceFromList(params DateTime[] source) => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}