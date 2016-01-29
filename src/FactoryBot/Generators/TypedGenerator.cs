using System;

namespace FactoryBot.Generators
{
    public abstract class TypedGenerator<T> : IGenerator
    {
        private readonly Random _random = new Random();

        public object Next()
        {
            return NextInternal();
        }

        protected abstract T NextInternal();

        protected int NextRandomInteger(int from, int to)
        {
            return _random.Next(from, to);
        }

        protected bool NextRandomBool()
        {
            return _random.Next(0, 1) == 1;
        }
    }
}