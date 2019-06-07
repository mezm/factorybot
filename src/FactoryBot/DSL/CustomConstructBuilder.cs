using FactoryBot.Generators;

namespace FactoryBot.DSL
{
    public class CustomConstructBuilder : BotConfigurationBuilder
    {
        [Generator(typeof(KeepGenerator))]
        public T Keep<T>() => default;
    }
}