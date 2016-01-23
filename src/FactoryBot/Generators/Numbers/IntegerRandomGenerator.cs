using System;

namespace FactoryBot.Generators.Numbers
{
    public class IntegerRandomGenerator : IGenerator
    {
        private readonly Random _random = new Random();
        private readonly int _from, _to;

        public IntegerRandomGenerator()
            : this(int.MinValue, int.MaxValue)
        {
        }

        public IntegerRandomGenerator(int from, int to)
        {
            _from = from;
            _to = to;
        }

        public object Next()
        {
            return _random.Next(_from, _to);
        }
    }
}