using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    public class IntegerGenerators 
    {
        [Generator(typeof(IntegerRandomGenerator))]
        public int Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter

        [Generator(typeof(IntegerRandomGenerator))]
        public int Any(int from, int to) => default;

        [Generator(typeof(RandomFromListGenerator<int>))]
        public int RandomFromList(IReadOnlyList<int> source) => default;

        [Generator(typeof(SequenceFromListGenerator<int>))]
        public int SequenceFromList(IReadOnlyList<int> source) => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}