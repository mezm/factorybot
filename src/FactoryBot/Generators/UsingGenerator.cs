namespace FactoryBot.Generators
{
    internal class UsingGenerator<T> : IGenerator
    {
        public object Next()
        {
            return Bot.Build<T>();
        }
    }
}