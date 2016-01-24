namespace FactoryBot.Generators
{
    public class GeneratorUsingDecorator<T> : IGenerator
    {
        public object Next()
        {
            return Bot.Build<T>();
        }
    }
}