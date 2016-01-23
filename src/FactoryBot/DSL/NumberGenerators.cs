using FactoryBot.Generators.Numbers;

namespace FactoryBot.DSL
{
    public class NumberGenerators
    {
        [Generator(typeof(IntegerRandomGenerator))]
        public int AnyInteger() => default(int);

        [Generator(typeof(IntegerRandomGenerator))]
        public int AnyInteger(int from, int to) => default(int);
    }
}