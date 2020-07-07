using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Byte generators
    /// </summary>
    public class ByteGenerators : IPrimitiveGenerators<byte, byte>
    {
#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random byte value
        /// </summary>
        /// <returns>Byte value</returns>
        [Generator(typeof(ByteRandomGenerator))]
        public byte Any() => default;

        /// <summary>
        /// Generates random byte value from given range
        /// </summary>
        /// <param name="from">Minimum value</param>
        /// <param name="to">Maximum value</param>
        /// <returns>Byte value</returns>
        [Generator(typeof(ByteRandomGenerator))]
        public byte Any(byte from, byte to) => default;

        /// <summary>
        /// Returns byte from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Byte value</returns>
        [Generator(typeof(RandomFromListGenerator<byte>))]
        public byte RandomFromList(IReadOnlyList<byte> source) => default;

        /// <summary>
        /// Returns byte from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>Byte value</returns>
        [Generator(typeof(RandomFromListGenerator<byte>))]
        public byte RandomFromList(params byte[] source) => default;

        /// <summary>
        /// Returns byte from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Byte value</returns>
        [Generator(typeof(SequenceFromListGenerator<byte>))]
        public byte SequenceFromList(IReadOnlyList<byte> source) => default;

        /// <summary>
        /// Returns byte from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>Byte value</returns>
        [Generator(typeof(SequenceFromListGenerator<byte>))]
        public byte SequenceFromList(params byte[] source) => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}