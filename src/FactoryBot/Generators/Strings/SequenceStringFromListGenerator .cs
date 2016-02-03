using System.Collections.Generic;

namespace FactoryBot.Generators.Strings
{
    public class SequenceStringFromListGenerator : TypedGenerator<string>
    {
        private readonly IReadOnlyList<string> _source;
        private int _index;

        public SequenceStringFromListGenerator(IReadOnlyList<string> source)
        {
            Check.NotNull(source, nameof(source));
            Check.CollectionNotEmpty(source, nameof(source));

            _source = source;
        }

        protected override string NextInternal()
        {
            if (_index >= _source.Count)
            {
                _index = 0;
            }

            return _source[_index++];
        }
    }
}