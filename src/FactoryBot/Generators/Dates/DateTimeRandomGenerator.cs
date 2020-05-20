using System;

namespace FactoryBot.Generators.Dates
{
    public class DateTimeRandomGenerator : TypedGenerator<DateTime>
    {
        private readonly long _from, _to;

        public DateTimeRandomGenerator(DateTime? from = null, DateTime? to = null)
        {
            from ??= new DateTime(1753, 1, 1);
            to ??= DateTime.MaxValue;

            if (from.Value > to.Value)
            {
                throw new ArgumentOutOfRangeException("Minimum should not be greater than maximum.");
            }

            _from = from.Value.Ticks;
            _to = to.Value.Ticks;
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