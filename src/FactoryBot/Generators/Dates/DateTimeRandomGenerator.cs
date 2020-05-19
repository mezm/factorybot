using System;

namespace FactoryBot.Generators.Dates
{
    public class DateTimeRandomGenerator : TypedGenerator<DateTime>
    {
        private readonly long _from, _to;

        public DateTimeRandomGenerator()
            : this(new DateTime(1753, 1, 1), DateTime.MaxValue)
        {
        }

        public DateTimeRandomGenerator(DateTime from, DateTime to)
        {
            Check.MinMax(from, to, nameof(from));

            _from = from.Ticks;
            _to = to.Ticks;
        }

        protected override DateTime NextInternal()
        {
            if (_from == _to)
            {
                return DateTime.FromBinary(_from);
            }

            var randomLong = (long)(NextRandomDouble() * (_to - _from) + _from);
            return DateTime.FromBinary(randomLong);
        }
    }
}