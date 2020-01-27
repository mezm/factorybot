namespace FactoryBot.Generators.Strings
{
    public class EmailGenerator : TypedGenerator<string>
    {
        private readonly WordRandomGenerator _wordGenerator = new WordRandomGenerator(1, 1);
        private readonly HostnameGenerator _hostnameGenerator = new HostnameGenerator();

        protected override string NextInternal()
        {
            var account = _wordGenerator.Next();
            var host = _hostnameGenerator.Next();
            return $"{account}@{host}";
        }
    }
}