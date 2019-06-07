using System.Collections.Generic;

using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL
{
    public class DecimalGenerators
    {
        [Generator(typeof(DecimalRandomGenerator))]
        public decimal Any() => default;

        [Generator(typeof(DecimalRandomGenerator))]
        public decimal Any(decimal from, decimal to) => default;

        [Generator(typeof(RandomFromListGenerator<decimal>))]
        public decimal RandomFromList(IReadOnlyList<decimal> source) => default;

        [Generator(typeof(SequenceFromListGenerator<decimal>))]
        public decimal SequenceFromList(IReadOnlyList<decimal> source) => default;
    }
}