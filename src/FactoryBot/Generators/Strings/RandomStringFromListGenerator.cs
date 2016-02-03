using System;
using System.Collections.Generic;

namespace FactoryBot.Generators.Strings
{
    public class RandomStringFromListGenerator : TypedGenerator<string>
    {
        private readonly IReadOnlyList<string> _source;

        public RandomStringFromListGenerator(IReadOnlyList<string> source)
        {
            Check.NotNull(source, nameof(source));
            if (source.Count == 0)
            {
                throw new ArgumentException("Source should not be empty.", nameof(source));
            }

            _source = source;
        }

        protected override string NextInternal()
        {
            var index = NextRandomInteger(0, _source.Count - 1);
            return _source[index];
        }
    }
}