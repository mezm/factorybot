﻿using System;

namespace FactoryBot.Generators
{
    public abstract class TypedGenerator<T> : IGenerator
        where T : notnull
    {
        private readonly Random _random = new Random((int)DateTime.Now.Ticks);

        public object Next() => NextInternal();

        protected abstract T NextInternal();

        protected int NextRandomInteger(int from, int to) => _random.Next(from, to + 1);

        protected double NextRandomDouble() => _random.NextDouble();

        protected bool NextRandomBool() => _random.NextDouble() >= 0.5;

        protected byte[] NextBytesRandom(int length)
        {
            var result = new byte[length];
            _random.NextBytes(result);
            return result;
        }

        protected TItem NextRandomFromArray<TItem>(TItem[] array) => array[NextRandomInteger(0, array.Length - 1)];
    }
}