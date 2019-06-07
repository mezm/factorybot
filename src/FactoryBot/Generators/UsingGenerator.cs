namespace FactoryBot.Generators
{
    internal class UsingGenerator<T> : IGenerator
    {
        public object Next() => Bot.Build<T>();
    }
}