using System;

namespace FactoryBot.Generators.Numbers
{
    public class LongRandomGenerator : TypedGenerator<long>
    {
        private readonly long _from, _to;

        public LongRandomGenerator(long from = long.MinValue, long to = int.MaxValue)
        {
            Check.MinMax(from, to, nameof(from));

            _from = from;
            _to = to;
        }

        protected override long NextInternal()
        {
            if (_to == _from) return _to;

            var bytes = NextBytesRandom(8);
            var value = BitConverter.ToInt64(bytes, 0);
            return Math.Abs(value % (_to - _from)) + _from;
        }
    }
}