namespace FactoryBot.Generators.Strings
{
    public class CountryGenerator : RandomLineFromResourceGenerator
    {
        public CountryGenerator() : base(SourceNames.COUNTRIES)
        {
        }
    }
}