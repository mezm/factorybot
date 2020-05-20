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

            if (from.Value > to.Value)
            {
                throw new ArgumentOutOfRangeException("Minimum should not be greater than maximum.");
            }

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
