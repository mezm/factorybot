using FactoryBot.Generators;
using FactoryBot.Generators.Collections;

namespace FactoryBot.DSL
{
    public class BotConfigurationBuilder
    {
        public NumberGenerators Numbers { get; } = new NumberGenerators();

        public StringGenerators Strings { get; } = new StringGenerators();

        public DateGenerators Dates { get; } = new DateGenerators();

        [Generator(typeof(GeneratorUsingDecorator<>))]
        public T Use<T>() => default(T);

        [Generator(typeof(ArrayGenerator<>))]
        public T[] Array<T>(int minElements, int maxElements, [ItemGenerator] T itemGenerator) => new T[0];
    }
}