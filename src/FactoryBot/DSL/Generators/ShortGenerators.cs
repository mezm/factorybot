using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    public class ShortGenerators : IPrimitiveGenerators<short, short>
    {
#pragma warning disable IDE0060 // Remove unused parameter
        [Generator(typeof(ShortRandomGenerator))]
        public short Any() => default;

        [Generator(typeof(ShortRandomGenerator))]
        public short Any(short from, short to) => default;

        [Generator(typeof(RandomFromListGenerator<short>))]
        public short RandomFromList(IReadOnlyList<short> source) => default;

        [Generator(typeof(RandomFromListGenerator<short>))]
        public short RandomFromList(params short[] source) => default;

        [Generator(typeof(SequenceFromListGenerator<short>))]
        public short SequenceFromList(IReadOnlyList<short> source) => default;

        [Generator(typeof(SequenceFromListGenerator<short>))]
        public short SequenceFromList(params short[] source) => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}