namespace FactoryBot.Generators.Numbers
{
    public class DoubleRandomGenerator : TypedGenerator<double>
    {
        private const int DefaultMinValue = -1000;
        private const int DefaultMaxValue = 1000;

        private readonly double _from, _to;

        public DoubleRandomGenerator(double from = DefaultMinValue, double to = DefaultMaxValue)
        {
            Check.MinMax(from, to, nameof(from));

            _from = from;
            _to = to;
        }

        protected override double NextInternal() => NextRandomDouble() * (_to - _from) + _from;
    }
}