using System;

namespace FactoryBot.Generators.Numbers
{
    public class FloatRandomGenerator : TypedGenerator<float>
    {
        private const float DEFAULT_MIN_VALUE = -1000;
        private const float DEFAULT_MAX_VALUE = 1000;

        private readonly DoubleRandomGenerator _doubleRandomGenerator;

        public FloatRandomGenerator(float from = DEFAULT_MIN_VALUE, float to = DEFAULT_MAX_VALUE)
        {
            if (from > to)
            {
                throw new ArgumentOutOfRangeException("Minimum should not be greater than maximum.");
            }

            _doubleRandomGenerator = new DoubleRandomGenerator(from, to);
        }

        protected override float NextInternal() => Convert.ToSingle(_doubleRandomGenerator.Next());
    }
}