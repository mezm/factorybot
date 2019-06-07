using System.Collections.Generic;

using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL
{
    public class IntegerGenerators 
    {
        [Generator(typeof(IntegerRandomGenerator))]
        public int Any() => default;

        [Generator(typeof(IntegerRandomGenerator))]
        public int Any(int from, int to) => default;

        [Generator(typeof(RandomFromListGenerator<int>))]
        public int RandomFromList(IReadOnlyList<int> source) => default;

        [Generator(typeof(SequenceFromListGenerator<int>))]
        public int SequenceFromList(IReadOnlyList<int> source) => default;
    }
}