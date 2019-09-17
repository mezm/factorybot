using System;

namespace FactoryBot.Generators
{
    internal class FactoryGenerator<T> : IGenerator
    {
        private readonly Func<T> _nextItem;

        public FactoryGenerator(Func<T> nextItem)
        {
            Check.NotNull(nextItem, nameof(nextItem));

            _nextItem = nextItem;
        }

        public object Next()
        {
            try
            {
                return _nextItem();
            }
            catch (Exception ex)
            {
                throw new BuildFailedException($"Failed to generate {typeof(T)} due to factory exception. See inner exception for more details", ex);
            }
        }
    }
}