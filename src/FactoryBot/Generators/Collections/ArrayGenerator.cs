namespace FactoryBot.Generators.Collections
{
    public class ArrayGenerator<T> : TypedGenerator<T[]>
    {
        private readonly int _minElements, _maxElements;
        private readonly IGenerator _itemGenerator;

        public ArrayGenerator(int minElements, int maxElements, IGenerator itemGenerator)
        {
            Check.MinMax(minElements, maxElements, nameof(minElements));
            Check.NotNull(itemGenerator, nameof(itemGenerator));

            _minElements = minElements;
            _maxElements = maxElements;
            _itemGenerator = itemGenerator;
        }

        protected override T[] NextInternal()
        {
            var length = NextRandomInteger(_minElements, _maxElements);
            var result = new T[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = (T)_itemGenerator.Next();
            }

            return result;
        }
    }
}