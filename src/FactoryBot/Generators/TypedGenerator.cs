using System;

namespace FactoryBot.Generators
{
    public abstract class TypedGenerator<T> : IGenerator
    {
        private readonly Random _random = new Random();

        public object Next() => NextInternal();

        protected abstract T NextInternal();

        protected int NextRandomInteger(int from, int to) => _random.Next(from, to);

        protected double NextRandomDouble() => _random.NextDouble();

        protected bool NextRandomBool() => _random.Next(0, 1) == 1;

        protected TItem NextRandomFromArray<TItem>(TItem[] array) => array[NextRandomInteger(0, array.Length)];
    }
}