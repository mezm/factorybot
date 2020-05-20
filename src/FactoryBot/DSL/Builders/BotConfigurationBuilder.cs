using System;
using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.DSL.Generators;
using FactoryBot.Generators;
using FactoryBot.Generators.Collections;

namespace FactoryBot.DSL.Builders
{
    public class BotConfigurationBuilder
    {
        /// <summary>
        /// Integer generators
        /// </summary>
        public IntegerGenerators Integer { get; } = new IntegerGenerators();

        /// <summary>
        /// Byte generators
        /// </summary>
        public ByteGenerators Byte { get; } = new ByteGenerators();

        /// <summary>
        /// Short generators
        /// </summary>
        public ShortGenerators Short { get; } = new ShortGenerators();

        /// <summary>
        /// Long generators
        /// </summary>
        public LongGenerators Long { get; } = new LongGenerators();

        /// <summary>
        /// String generators
        /// </summary>
        public StringGenerators Strings { get; } = new StringGenerators();

        /// <summary>
        /// Date generators
        /// </summary>
        public DateGenerators Dates { get; } = new DateGenerators();

        /// <summary>
        /// Time generators
        /// </summary>
        public TimeGenerators Time { get; } = new TimeGenerators();

        /// <summary>
        /// Decimal generators
        /// </summary>
        public DecimalGenerators Decimal { get; } = new DecimalGenerators();

        /// <summary>
        /// Double generators
        /// </summary>
        public DoubleGenerators Double { get; } = new DoubleGenerators();

        /// <summary>
        /// Float generators
        /// </summary>
        public FloatGenerators Float { get; } = new FloatGenerators();

        /// <summary>
        /// Boolean generators
        /// </summary>
        public BooleanGenerators Boolean { get; } = new BooleanGenerators();

        /// <summary>
        /// Name related generators
        /// </summary>
        public NameGenerators Names { get; } = new NameGenerators();

        /// <summary>
        /// Address related generator
        /// </summary>
        public AddressGenerators Address { get; } = new AddressGenerators();

        /// <summary>
        /// Network related (URL, email, host, etc.) generators
        /// </summary>
        public NetworkGenerators Network { get; } = new NetworkGenerators();

        [Generator(typeof(UsingGenerator<>))]
        public T Use<T>() => default!;

#pragma warning disable IDE0060 // Remove unused parameter

        /// <summary>
        /// Generates value based on callback passed into the method.
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="nextItem">Callback that is going to be called each time generator needs new value</param>
        /// <returns>Generated value</returns>
        [Generator(typeof(FactoryGenerator<>))]
        public T Factory<T>(Func<T> nextItem) => default!;

        /// <summary>
        /// Generates array of random items of type T.
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="minElements">Min length of generated array</param>
        /// <param name="maxElements">Max length of generated array</param>
        /// <param name="itemGenerator">Array item generator</param>
        /// <returns>Array of random items</returns>
        [Generator(typeof(ArrayGenerator<>))]
        public T[] Array<T>(int minElements, int maxElements, [ItemGenerator] T itemGenerator) => new T[0];

        /// <summary>
        /// Generates array of random items of type T. The same as Array(minElements, maxElements, Use&lt;T&gt;()).
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="minElements">Min length of generated array</param>
        /// <param name="maxElements">Max length of generated array</param>
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

        /// <summary>
        /// Generates dictionary of random key value pairs.
        /// </summary>
        /// <typeparam name="TKey">Dictionary key type</typeparam>
        /// <typeparam name="TValue">Dictionary value type</typeparam>
        /// <param name="minElements">Min length of generated dictionary</param>
        /// <param name="maxElements">Max length of generated dictionary</param>
        /// <param name="keyGenerator">Key generator</param>
        /// <param name="valueGenerator">Value generator</param>
        /// <returns>Dictionary of random key value pairs</returns>
        [Generator(typeof(DictionaryGenerator<,>))]
        public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int minElements, int maxElements, [ItemGenerator] TKey keyGenerator, [ItemGenerator] TValue valueGenerator) => new Dictionary<TKey, TValue>();

        /// <summary>
        /// Generates dictionary of random key value pairs. The same as Dictionary(minElements, maxElements, Use&lt;TKey&gt;(), Use&lt;TValue&gt;()).
        /// </summary>
        /// <typeparam name="TKey">Dictionary key type</typeparam>
        /// <typeparam name="TValue">Dictionary value type</typeparam>
        /// <param name="minElements">Min length of generated dictionary</param>
        /// <param name="maxElements">Max length of generated dictionary</param>
        /// <returns>Dictionary of random key value pairs</returns>
        [Generator(typeof(DictionaryGenerator<,>))]
        [UseDefaultItemGenerator("keyGenerator")]
        [UseDefaultItemGenerator("valueGenerator", 1)]
        public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int minElements, int maxElements) => new Dictionary<TKey, TValue>();

#pragma warning restore IDE0060 // Remove unused parameter
    }
}