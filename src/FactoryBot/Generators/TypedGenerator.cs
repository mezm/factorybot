namespace FactoryBot.Generators
{
    public abstract class TypedGenerator<T> : IGenerator
    {
        public object Next()
        {
            return NextInternal();
        }

        protected abstract T NextInternal();
    }
}