using System;

namespace FactoryBot.Generators.Numbers
{
    public class ShortRandomGenerator : TypedGenerator<short>
    {
        private readonly IntegerRandomGenerator _integerRandomGenerator;

        public ShortRandomGenerator(short from = short.MinValue, short to = short.MaxValue)
        {
            if (from > to)
            {
                throw new ArgumentOutOfRangeException("Minimum should not be greater than maximum.");
            }

            _integerRandomGenerator = new IntegerRandomGenerator(from, to);
        }

        protected override short NextInternal() => Convert.ToInt16(_integerRandomGenerator.Next());
    }
}