namespace FactoryBot.Generators.Strings
{
    public class LastNameGenerator : RandomLineFromResourceGenerator
    {
        public LastNameGenerator() : base(SourceNames.LastNames)
        {
        }
    }
}