using System.Linq;

namespace FactoryBot.Generators.Strings
{
    public class HostnameGenerator : TypedGenerator<string>
    {
        private readonly IGenerator _tldGenerator = ResourceBasedGeneratorFactory.CreateTopLevelDomainGenerator();
        private readonly WordRandomGenerator _wordGenerator = WordRandomGenerator.CreateSingleWordGenerator();
        private readonly int _minSubdomains;
        private readonly int _maxSubdomains;

        public HostnameGenerator(int minSubdomains = 1, int maxSubdomains = 4)
        {
            _minSubdomains = minSubdomains;
            _maxSubdomains = maxSubdomains;
        }

        protected override string NextInternal()
        {
            var subdomainCount = NextRandomInteger(_minSubdomains, _maxSubdomains);
            var subdomains = Enumerable.Range(0, subdomainCount).Select(_ => _wordGenerator.Next());
            var tld = _tldGenerator.Next();
            return string.Join(".", subdomains) + "." + tld;
        }
    }
}