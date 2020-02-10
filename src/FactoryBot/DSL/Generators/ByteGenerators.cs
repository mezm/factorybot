using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    public class ByteGenerators : IPrimitiveGenerators<byte, byte>
    {
#pragma warning disable IDE0060 // Remove unused parameter
        [Generator(typeof(ByteRandomGenerator))]
        public byte Any() => default;

        [Generator(typeof(ByteRandomGenerator))]
        public byte Any(byte from, byte to) => default;

        [Generator(typeof(RandomFromListGenerator<byte>))]
        public byte RandomFromList(IReadOnlyList<byte> source) => default;

        [Generator(typeof(RandomFromListGenerator<byte>))]
        public byte RandomFromList(params byte[] source) => default;

        [Generator(typeof(SequenceFromListGenerator<byte>))]
        public byte SequenceFromList(IReadOnlyList<byte> source) => default;

        [Generator(typeof(SequenceFromListGenerator<byte>))]
        public byte SequenceFromList(params byte[] source) => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}