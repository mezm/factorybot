using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    public class FloatGenerators : IPrimitiveGenerators<float, float>
    {
#pragma warning disable IDE0060 // Remove unused parameter
        [Generator(typeof(FloatRandomGenerator))]
        public float Any() => default;

        [Generator(typeof(FloatRandomGenerator))]
        public float Any(float from, float to) => default;

        [Generator(typeof(RandomFromListGenerator<float>))]
        public float RandomFromList(IReadOnlyList<float> source) => default;

        [Generator(typeof(RandomFromListGenerator<float>))]
        public float RandomFromList(params float[] source) => default;

        [Generator(typeof(SequenceFromListGenerator<float>))]
        public float SequenceFromList(IReadOnlyList<float> source) => default;

        [Generator(typeof(SequenceFromListGenerator<float>))]
        public float SequenceFromList(params float[] source) => default;
#pragma warning restore IDE0060 // Remove unused parameter
    }
}