using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Guids;
using System;
using System.Collections.Generic;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// GUID generators
    /// </summary>
    public class GuidGenerators
    {
#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random GUID
        /// </summary>
        /// <returns>GUID value</returns>
        [Generator(typeof(GuidRandomGenerator))]
        public Guid Any() => default;

        /// <summary>
        /// Returns GUID from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>GUID value</returns>
        [Generator(typeof(RandomFromListGenerator<Guid>))]
        public Guid RandomFromList(IReadOnlyList<Guid> source) => default;

        /// <summary>
        /// Returns GUID from a collection in random order
        /// </summary>
        /// <param name="source">The collection</param>
        /// <returns>GUID value</returns>
        [Generator(typeof(RandomFromListGenerator<Guid>))]
        public Guid RandomFromList(params Guid[] source) => default;

        /// <summary>
        /// Returns GUID from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>GUID value</returns>
        [Generator(typeof(SequenceFromListGenerator<Guid>))]
        public Guid SequenceFromList(IReadOnlyList<Guid> source) => default;

        /// <summary>
        /// Returns GUID from a collection in the same order they defined
        /// </summary>
        /// <param name="source">The sequence</param>
        /// <returns>GUID value</returns>
        [Generator(typeof(SequenceFromListGenerator<Guid>))]
        public Guid SequenceFromList(params Guid[] source) => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}