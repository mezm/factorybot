using System.Collections.Generic;

using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL
{
    public class IntegerGenerators 
    {
        [Generator(typeof(IntegerRandomGenerator))]
        public int Any() => default(int);

        [Generator(typeof(IntegerRandomGenerator))]
        public int Any(int from, int to) => default(int);

        [Generator(typeof(RandomFromListGenerator<int>))]
        public int RandomFromList(IReadOnlyList<int> source) => default(int);

        [Generator(typeof(SequenceFromListGenerator<int>))]
        public int SequenceFromList(IReadOnlyList<int> source) => default(int);
    }
}