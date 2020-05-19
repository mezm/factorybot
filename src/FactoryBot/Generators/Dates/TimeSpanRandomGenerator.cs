using System;

namespace FactoryBot.Generators.Dates
{
    public class TimeSpanRandomGenerator : TypedGenerator<TimeSpan>
    {
        private readonly long _from, _to;

        public TimeSpanRandomGenerator(TimeSpan? from = null, TimeSpan? to = null)
        {
            from ??= TimeSpan.Zero;
            to ??= TimeSpan.MaxValue;

            Check.MinMax(from.Value, to.Value, nameof(from));

            _from = from.Value.Ticks;
            _to = to.Value.Ticks;
        }

        protected override TimeSpan NextInternal()
        {
            var ticks = _from == _to ? _from : (long)(NextRandomDouble() * (_to - _from) + _from);
            return TimeSpan.FromTicks(ticks);
        }
    }
}
