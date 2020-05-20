using System;
using System.Collections.Generic;

namespace FactoryBot.Generators.Collections
{
    public class SequenceFromListGenerator<T> : TypedGenerator<T>
        where T : notnull
    {
        private readonly IReadOnlyList<T> _source;
        private int _index;

        public SequenceFromListGenerator(IReadOnlyList<T> source)
        {
            if (source.Count == 0)
            {
                throw new ArgumentException("Collection should not be empty.", nameof(source));
            }

            _source = source;
        }

        protected override T NextInternal()
        {
            if (_index >= _source.Count)
            {
                _index = 0;
            }

            return _source[_index++];
        }
    }
}