using System.Collections.Generic;

namespace FactoryBot.Generators.Collections
{
    public abstract class BaseCollectionGenerator<TItem, TCollection> : TypedGenerator<TCollection> 
        where TCollection : IEnumerable<TItem>
    {
        private const int TRIES_FACTOR = 10;

        private readonly int _minElements, _maxElements;
        private readonly IGenerator _itemGenerator;

        protected BaseCollectionGenerator(int minElements, int maxElements, IGenerator itemGenerator)
        {
            Check.MinMax(minElements, maxElements, nameof(minElements));
            Check.NotNull(itemGenerator, nameof(itemGenerator));

            _minElements = minElements;
            _maxElements = maxElements;
            _itemGenerator = itemGenerator;
        }

        protected override TCollection NextInternal()
        {
            var length = NextRandomInteger(_minElements, _maxElements);
            var result = CreateNewEmptyCollection(length);

            var currentLength = 0;
            var maxTries = length * TRIES_FACTOR;
            for (var tries = 0 ; currentLength < length; tries++)
            {
                currentLength = AddItemToCollection(result, currentLength, (TItem)_itemGenerator.Next());
                if (tries >= maxTries)
                {
                    throw new BuildFailedException($"Failed to generate collection {typeof(TCollection)} of length {length} due to items not added. Tried to add items {tries} tiems.");
                }
            }

            return result;
        }

        protected abstract TCollection CreateNewEmptyCollection(int length);

        protected abstract int AddItemToCollection(TCollection collection, int index, TItem item);
    }
}