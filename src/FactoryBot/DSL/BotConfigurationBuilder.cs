using System.Collections.Generic;

using FactoryBot.Generators;
using FactoryBot.Generators.Collections;

namespace FactoryBot.DSL
{
    public class BotConfigurationBuilder
    {
        public IntegerGenerators Integer { get; } = new IntegerGenerators();
        public StringGenerators Strings { get; } = new StringGenerators();
        public DateGenerators Dates { get; } = new DateGenerators();
        public DecimalGenerators Decimal { get; } = new DecimalGenerators();
        public DoubleGenerators Double { get; } = new DoubleGenerators();

        [Generator(typeof(UsingGenerator<>))]
        public T Use<T>() => default;

        [Generator(typeof(ArrayGenerator<>))]
        public T[] Array<T>(int minElements, int maxElements, [ItemGenerator] T itemGenerator) => new T[0];

        [Generator(typeof(ListGenerator<>))]
        public List<T> List<T>(int minElements, int maxElements, [ItemGenerator] T itemGenerator) => new List<T>();

        [Generator(typeof(DictionaryGenerator<,>))]
        public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int minElements, int maxElements, [ItemGenerator] TKey keyGenerator, [ItemGenerator] TValue valueGenerator) => new Dictionary<TKey, TValue>();
    }
}