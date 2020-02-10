using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    public class LongGenerators : IPrimitiveGenerators<long, long>
    {
#pragma warning disable IDE0060 // Remove unused parameter
        [Generator(typeof(LongRandomGenerator))]
        public long Any() => default;

        [Generator(typeof(LongRandomGenerator))]
        public long Any(long from, long to) => default;

        [Generator(typeof(RandomFromListGenerator<long>))]
        public long RandomFromList(IReadOnlyList<long> source) => default;

        [Generator(typeof(RandomFromListGenerator<long>))]
        public long RandomFromList(params long[] source) => default;

        [Generator(typeof(SequenceFromListGenerator<long>))]
        public long SequenceFromList(IReadOnlyList<long> source) => default;

        [Generator(typeof(SequenceFromListGenerator<long>))]
        public long SequenceFromList(params long[] source) => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}