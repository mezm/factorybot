using FactoryBot.Generators;

namespace FactoryBot.DSL
{
    public class CustomConstructBuilder
    {
        public BotConfigurationBuilder Builder { get; } = new BotConfigurationBuilder();

        [Generator(typeof(KeepGenerator))]
        public T Keep<T>() => default(T);
    }
}