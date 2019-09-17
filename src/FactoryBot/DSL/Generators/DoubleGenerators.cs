using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL.Generators
{
    public class DoubleGenerators : IPrimitiveGenerators<double, double>
    {
        [Generator(typeof(DoubleRandomGenerator))]
        public double Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter

        [Generator(typeof(DoubleRandomGenerator))]
        public double Any(double from, double to) => default;

        [Generator(typeof(RandomFromListGenerator<double>))]
        public double RandomFromList(IReadOnlyList<double> source) => default;

        [Generator(typeof(RandomFromListGenerator<double>))]
        public double RandomFromList(params double[] source) => default;

        [Generator(typeof(SequenceFromListGenerator<double>))]
        public double SequenceFromList(IReadOnlyList<double> source) => default;

        [Generator(typeof(SequenceFromListGenerator<double>))]
        public double SequenceFromList(params double[] source) => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}