using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.DSL.Generators;
using FactoryBot.Generators;
using FactoryBot.Generators.Collections;

namespace FactoryBot.DSL.Builders
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

#pragma warning disable IDE0060 // Remove unused parameter

        /// <summary>
        /// Generates array of random items of type T.
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="minElements">Min length of generated list</param>
        /// <param name="maxElements">Max length of generated list</param>
        /// <param name="itemGenerator">List item generator</param>
        /// <returns>Array of random items</returns>
        [Generator(typeof(ArrayGenerator<>))]
        public T[] Array<T>(int minElements, int maxElements, [ItemGenerator] T itemGenerator) => new T[0];

        /// <summary>
        /// Generates array of random items of type T. The same as Array(minElements, maxElements, Use&lt;T&gt;()).
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="minElements">Min length of generated list</param>
        /// <param name="maxElements">Max length of generated list</param>
        /// <returns>Array of random items</returns>
        [Generator(typeof(ArrayGenerator<>))]
        [UseDefaultItemGenerator("itemGenerator")]
        public T[] Array<T>(int minElements, int maxElements) => new T[0];

        /// <summary>
        /// Generates list of items of type T.
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="minElements">Min length of generated list</param>
        /// <param name="maxElements">Max length of generated list</param>
        /// <param name="itemGenerator">List item generator</param>
        /// <returns>List of random items</returns>
        [Generator(typeof(ListGenerator<>))]
        public List<T> List<T>(int minElements, int maxElements, [ItemGenerator] T itemGenerator) => new List<T>();

        /// <summary>
        /// Generates list of items of type T. The same as List(minElements, maxElements, Use&lt;T&gt;())
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="minElements">Min length of generated list</param>
        /// <param name="maxElements">Max length of generated list</param>
        /// <returns>List of random items</returns>
        [Generator(typeof(ListGenerator<>))]
        [UseDefaultItemGenerator("itemGenerator")]
        public List<T> List<T>(int minElements, int maxElements) => new List<T>();

        [Generator(typeof(DictionaryGenerator<,>))]
        public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int minElements, int maxElements, [ItemGenerator] TKey keyGenerator, [ItemGenerator] TValue valueGenerator) => new Dictionary<TKey, TValue>();

#pragma warning restore IDE0060 // Remove unused parameter
    }
}