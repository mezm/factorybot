using System.Collections.Generic;

using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL
{
    public class DoubleGenerators
    {
        [Generator(typeof(DoubleRandomGenerator))]
        public double Any() => default(double);

        [Generator(typeof(DoubleRandomGenerator))]
        public double Any(double from, double to) => default(double);

        [Generator(typeof(RandomFromListGenerator<double>))]
        public double RandomFromList(IReadOnlyList<double> source) => default(double);

        [Generator(typeof(SequenceFromListGenerator<double>))]
        public double SequenceFromList(IReadOnlyList<double> source) => default(double);
    }
}