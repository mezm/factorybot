using System;
using System.Collections.Generic;

using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Dates;

namespace FactoryBot.DSL
{
    public class DateGenerators
    {
        [Generator(typeof(RandomDateTimeGenerator))]
        public DateTime Any() => default(DateTime);

        [Generator(typeof(RandomDateTimeGenerator))]
        public DateTime Any(DateTime from, DateTime to) => default(DateTime);

        [Generator(typeof(RandomFromListGenerator<DateTime>))]
        public DateTime RandomFromList(IReadOnlyList<DateTime> source) => default(DateTime);

        [Generator(typeof(SequenceFromListGenerator<DateTime>))]
        public DateTime SequenceFromList(IReadOnlyList<int> source) => default(DateTime);
    }
}