namespace FactoryBot.Generators.Strings
{
    public class FirstNameGenerator : RandomLineFromFileGenerator
    {
        public FirstNameGenerator() : base(SourceNames.FirstNames)
        {
        }
    }
}