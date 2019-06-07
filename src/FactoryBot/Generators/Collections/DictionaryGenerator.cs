using System;
using System.Collections.Generic;

namespace FactoryBot.Generators.Collections
{
    public class DictionaryGenerator<TKey, TValue> : BaseCollectionGenerator<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>>
    {
        public DictionaryGenerator(int minElements, int maxElements, IGenerator keyGenerator, IGenerator valueGenerator) 
            : base(minElements, maxElements, new KeyValuePairGenerator(keyGenerator, valueGenerator))
        {
        }

        protected override void AddItemToCollection(Dictionary<TKey, TValue> collection, int index, KeyValuePair<TKey, TValue> item) => collection[item.Key] = item.Value;

        protected override Dictionary<TKey, TValue> CreateNewEmptyCollection(int length) => new Dictionary<TKey, TValue>(length);

        private class KeyValuePairGenerator : IGenerator
        {
            private readonly IGenerator _keyGenerator, _valueGenerator;

            public KeyValuePairGenerator(IGenerator keyGenerator, IGenerator valueGenerator)
            {
                _keyGenerator = keyGenerator ?? throw new ArgumentNullException(nameof(keyGenerator));
                _valueGenerator = valueGenerator ?? throw new ArgumentNullException(nameof(valueGenerator));
            }

            public object Next() => new KeyValuePair<TKey, TValue>((TKey)_keyGenerator.Next(), (TValue)_valueGenerator.Next());
        }
    }
}
