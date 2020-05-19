using System;
using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Dates;

namespace FactoryBot.DSL.Generators
{
    public class TimeGenerators : IPrimitiveGenerators<TimeSpan, TimeSpan>
    {
        [Generator(typeof(TimeSpanRandomGenerator))]
        public TimeSpan Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter

        [Generator(typeof(TimeSpanRandomGenerator))]
        public TimeSpan Any(TimeSpan from, TimeSpan to) => default;

        [Generator(typeof(RandomFromListGenerator<TimeSpan>))]
        public TimeSpan RandomFromList(IReadOnlyList<TimeSpan> source) => default;

        [Generator(typeof(RandomFromListGenerator<TimeSpan>))]
        public TimeSpan RandomFromList(params TimeSpan[] source) => default;

        [Generator(typeof(SequenceFromListGenerator<TimeSpan>))]
        public TimeSpan SequenceFromList(IReadOnlyList<TimeSpan> source) => default;

        [Generator(typeof(SequenceFromListGenerator<TimeSpan>))]
        public TimeSpan SequenceFromList(params TimeSpan[] source) => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}