using System.Collections.Generic;

namespace FactoryBot.Generators.Collections
{
    public abstract class BaseCollectionGenerator<TItem, TCollection> : TypedGenerator<TCollection> 
        where TCollection : IEnumerable<TItem>
    {
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
            for (var i = 0; i < length; i++)
            {
                AddItemToCollection(result, i, (TItem)_itemGenerator.Next());
            }

            // todo: does not guarantee that length in [min, max]. should be fixed!
            return result;
        }

        protected abstract TCollection CreateNewEmptyCollection(int length);

        protected abstract void AddItemToCollection(TCollection collection, int index, TItem item);
    }
}