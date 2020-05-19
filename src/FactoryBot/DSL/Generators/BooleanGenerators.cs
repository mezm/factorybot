using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    public class BooleanGenerators
    {
        [Generator(typeof(BooleanRandomGenerator))]
        public bool Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter
        [Generator(typeof(SequenceFromListGenerator<bool>))]
        public byte SequenceFromList(IReadOnlyList<bool> source) => default;

        [Generator(typeof(SequenceFromListGenerator<byte>))]
        public byte SequenceFromList(params bool[] source) => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}