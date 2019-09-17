using FactoryBot.DSL.Attributes;
using FactoryBot.Generators;

namespace FactoryBot.DSL.Builders
{
    public class CustomConstructBuilder : BotConfigurationBuilder
    {
        [Generator(typeof(KeepGenerator))]
        public T Keep<T>() => default;
    }
}