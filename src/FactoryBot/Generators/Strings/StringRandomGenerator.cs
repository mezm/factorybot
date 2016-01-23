namespace FactoryBot.Generators.Strings
{
    public class StringRandomGenerator : IGenerator
    {
        public object Next()
        {
            return "abc";
        }
    }
}