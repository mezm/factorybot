using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    public class DecimalGenerators
    {
        [Generator(typeof(DecimalRandomGenerator))]
        public decimal Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter

        [Generator(typeof(DecimalRandomGenerator))]
        public decimal Any(decimal from, decimal to) => default;

        [Generator(typeof(RandomFromListGenerator<decimal>))]
        public decimal RandomFromList(IReadOnlyList<decimal> source) => default;

        [Generator(typeof(RandomFromListGenerator<decimal>))]
        public decimal RandomFromList(params decimal[] source) => default;

        [Generator(typeof(SequenceFromListGenerator<decimal>))]
        public decimal SequenceFromList(IReadOnlyList<decimal> source) => default;

        [Generator(typeof(SequenceFromListGenerator<decimal>))]
        public decimal SequenceFromList(params decimal[] source) => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}