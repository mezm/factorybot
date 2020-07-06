using System;
using System.Linq;

namespace FactoryBot.Generators.Enums
{
    public class EnumRandomGenerator<T> : TypedGenerator<T>
        where T : Enum
    {
        private readonly T[] _allValues = Enum.GetValues(typeof(T)).Cast<T>().ToArray();

        protected override T NextInternal() => NextRandomFromArray(_allValues);
    }
}
