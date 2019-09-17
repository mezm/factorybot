namespace FactoryBot.Generators.Collections
{
    public class ArrayGenerator<T> : BaseCollectionGenerator<T, T[]>
    {
        public ArrayGenerator(int minElements, int maxElements, IGenerator itemGenerator)
            : base(minElements, maxElements, itemGenerator)
        {
        }

        protected override T[] CreateNewEmptyCollection(int length) => new T[length];

        protected override int AddItemToCollection(T[] collection, int index, T item)
        {
            collection[index] = item;
            return index + 1;
        }
    }
}