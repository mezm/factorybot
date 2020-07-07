using System;

namespace FactoryBot.Generators.Numbers
{
    public class IntegerRandomGenerator : TypedGenerator<int>
    {
        private readonly int _from, _to;

        public IntegerRandomGenerator(int from = int.MinValue, int to = int.MaxValue - 1)
        {
            if (from > to)
            {
                throw new ArgumentOutOfRangeException("Minimum should not be greater than maximum.");
            }

            _from = from;
            _to = to;
        }

        protected override int NextInternal() => NextRandomInteger(_from, _to);
    }
}