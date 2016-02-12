namespace FactoryBot.Generators.Strings
{
    public class FirstNameGenerator : RandomLineFromResourceGenerator
    {
        public FirstNameGenerator() : base(SourceNames.FirstNames)
        {
        }
    }
}