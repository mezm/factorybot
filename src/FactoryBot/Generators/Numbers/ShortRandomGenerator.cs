using System;

namespace FactoryBot.Generators.Numbers
{
    public class ShortRandomGenerator : TypedGenerator<short>
    {
        private readonly IntegerRandomGenerator _integerRandomGenerator;

        public ShortRandomGenerator(short from = short.MinValue, short to = short.MaxValue)
        {
            Check.MinMax(from, to, nameof(from));

            _integerRandomGenerator = new IntegerRandomGenerator(from, to);
        }

        protected override short NextInternal() => Convert.ToInt16(_integerRandomGenerator.Next());
    }
}