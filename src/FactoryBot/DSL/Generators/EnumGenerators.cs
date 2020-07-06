using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Enums;
using System;
using System.Collections.Generic;

namespace FactoryBot.DSL.Generators
{
    public class EnumGenerators 
    {
#pragma warning disable IDE0060 // Remove unused parameter
        [Generator(typeof(EnumRandomGenerator<>))]
        public T Any<T>() where T : notnull, Enum => default!;

        [Generator(typeof(RandomFromListGenerator<Enum>))]
        public double RandomFromList<T>(IReadOnlyList<T> source) where T : notnull, Enum => default;

        [Generator(typeof(RandomFromListGenerator<Enum>))]
        public double RandomFromList<T>(params T[] source) where T : notnull, Enum => default;

        [Generator(typeof(SequenceFromListGenerator<Enum>))]
        public double SequenceFromList<T>(IReadOnlyList<T> source) where T : notnull, Enum => default;

        [Generator(typeof(SequenceFromListGenerator<Enum>))]
        public double SequenceFromList<T>(params T[] source) where T : notnull, Enum => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}