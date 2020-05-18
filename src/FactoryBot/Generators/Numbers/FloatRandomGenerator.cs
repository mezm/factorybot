using System;

namespace FactoryBot.Generators.Numbers
{
    public class FloatRandomGenerator : TypedGenerator<float>
    {
        private readonly DoubleRandomGenerator _doubleRandomGenerator;

        public FloatRandomGenerator(float from = float.MinValue, float to = float.MaxValue)
        {
            Check.MinMax(from, to, nameof(from));

            _doubleRandomGenerator = new DoubleRandomGenerator(from, to);
        }

        protected override float NextInternal() => Convert.ToSingle(_doubleRandomGenerator.Next());
    }
}