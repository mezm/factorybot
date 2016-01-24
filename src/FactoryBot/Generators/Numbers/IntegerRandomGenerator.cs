using System;

namespace FactoryBot.Generators.Numbers
{
    public class IntegerRandomGenerator : TypedGenerator<int>
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
        
        protected override int NextInternal()
        {
            return _random.Next(_from, _to);
        }
    }
}