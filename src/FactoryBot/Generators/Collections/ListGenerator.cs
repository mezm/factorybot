using System.Collections.Generic;

namespace FactoryBot.Generators.Collections
{
    public class ListGenerator<T> : BaseCollectionGenerator<T, List<T>>
    {
        public ListGenerator(int minElements, int maxElements, IGenerator itemGenerator)
            : base(minElements, maxElements, itemGenerator)
        {
        }

        protected override List<T> CreateNewEmptyCollection(int length) => new List<T>(length);

        protected override void AddItemToCollection(List<T> collection, int index, T item) => collection.Add(item);
    }
}