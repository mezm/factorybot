using System.Collections.Generic;

namespace FactoryBot.Generators.Collections
{
    public class RandomFromListGenerator<T> : TypedGenerator<T>
        where T : notnull
    {
        private readonly IReadOnlyList<T> _source;

        public RandomFromListGenerator(IReadOnlyList<T> source)
        {
            Check.NotNull(source, nameof(source));
            Check.CollectionNotEmpty(source, nameof(source));

            _source = source;
        }

        protected override T NextInternal()
        {
            var index = NextRandomInteger(0, _source.Count - 1);
            return _source[index];
        }
    }
}