using System;

namespace FactoryBot.Generators.Numbers
{
    public class ByteRandomGenerator : TypedGenerator<byte>
    {
        private readonly IntegerRandomGenerator _integerRandomGenerator;

        public ByteRandomGenerator(byte from = byte.MinValue, byte to = byte.MaxValue)
        {
            if (from > to)
            {
                throw new ArgumentOutOfRangeException("Minimum should not be greater than maximum.");
            }

            _integerRandomGenerator = new IntegerRandomGenerator(from, to);
        }

        protected override byte NextInternal() => Convert.ToByte(_integerRandomGenerator.Next());
    }
}