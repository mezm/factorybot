namespace FactoryBot.DSL
{
    public class BotConfigurationBuilder
    {
        public NumberGenerators Numbers { get; } = new NumberGenerators();

        public StringGenerators Strings { get; } = new StringGenerators();

        public DateGenerators Dates { get; } = new DateGenerators();
    }
}