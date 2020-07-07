using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Enums;
using System;
using System.Collections.Generic;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Enum generators
    /// </summary>
    public class EnumGenerators 
    {
#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random enum value
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <returns>Enum value</returns>
        [Generator(typeof(EnumRandomGenerator<>))]
        public T Any<T>() where T : notnull, Enum => default!;

        /// <summary>
        /// Returns enum value from a collection in random order
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="source">The collection</param>
        /// <returns>Enum value</returns>
        [Generator(typeof(RandomFromListGenerator<>))]
        public T RandomFromList<T>(IReadOnlyList<T> source) where T : notnull, Enum => default!;

        /// <summary>
        /// Returns enum value from a collection in random order
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="source">The collection</param>
        /// <returns>Enum value</returns>
        [Generator(typeof(RandomFromListGenerator<>))]
        public T RandomFromList<T>(params T[] source) where T : notnull, Enum => default!;

        /// <summary>
        /// Returns enum value from a collection in the same order they defined
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="source">The sequence</param>
        /// <returns>Enum value</returns>
        [Generator(typeof(SequenceFromListGenerator<>))]
        public T SequenceFromList<T>(IReadOnlyList<T> source) where T : notnull, Enum => default!;

        /// <summary>
        /// Returns enum value from a collection in the same order they defined
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="source">The sequence</param>
        /// <returns>Enum value</returns>
        [Generator(typeof(SequenceFromListGenerator<>))]
        public T SequenceFromList<T>(params T[] source) where T : notnull, Enum => default!;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}