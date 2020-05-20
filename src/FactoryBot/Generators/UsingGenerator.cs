namespace FactoryBot.Generators
{
    internal class UsingGenerator<T> : IGenerator
        where T : notnull
    {
        public object Next() => Bot.Build<T>();
    }
}