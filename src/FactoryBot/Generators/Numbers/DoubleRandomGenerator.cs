namespace FactoryBot.Generators.Numbers
{
    public class DoubleRandomGenerator : TypedGenerator<double>
    {
        private const int DEFAULT_MIN_VALUE = -1000;
        private const int DEFAULT_MAX_VALUE = 1000;

        private readonly double _from, _to;

        public DoubleRandomGenerator(double from = DEFAULT_MIN_VALUE, double to = DEFAULT_MAX_VALUE)
        {
            Check.MinMax(from, to, nameof(from));

            _from = from;
            _to = to;
        }

        protected override double NextInternal() => NextRandomDouble() * (_to - _from) + _from;
    }
}