namespace FactoryBot.Generators.Strings
{
    public class LastNameGenerator : RandomLineFromFileGenerator
    {
        public LastNameGenerator() : base(SourceNames.LastNames)
        {
        }
    }
}