using System;

namespace FactoryBot.Generators.Numbers
{
    public class DecimalRandomGenerator : TypedGenerator<decimal>
    {
        private readonly DoubleRandomGenerator _doubleRandomGenerator;

        public DecimalRandomGenerator(decimal from = decimal.MinValue, decimal to = decimal.MaxValue)
        {
            if (from > to)
            {
                throw new ArgumentOutOfRangeException("Minimum should not be greater than maximum.");
            }

            _doubleRandomGenerator = new DoubleRandomGenerator((double)from, (double)to);
        }

        protected override decimal NextInternal() => new decimal((double)_doubleRandomGenerator.Next());
    }
}