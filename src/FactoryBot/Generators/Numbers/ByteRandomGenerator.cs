using System;

namespace FactoryBot.Generators.Numbers
{
    public class ByteRandomGenerator : TypedGenerator<byte>
    {
        private readonly IntegerRandomGenerator _integerRandomGenerator;

        public ByteRandomGenerator(byte from = byte.MinValue, byte to = byte.MaxValue)
        {
            Check.MinMax(from, to, nameof(from));

            _integerRandomGenerator = new IntegerRandomGenerator(from, to);
        }

        protected override byte NextInternal() => Convert.ToByte(_integerRandomGenerator.Next());
    }
}