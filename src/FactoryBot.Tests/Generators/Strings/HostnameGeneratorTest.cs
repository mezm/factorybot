using FactoryBot.Generators.Strings;
using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class HostnameGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Hostname_NoCondition_ReturnsNewHostname() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Network.Hostname() });

        [Test]
        public void Hostname_SingleSubdomain_ReturnsHostname() 
        {
            var generator = new HostnameGenerator(1, 1);

            var host = generator.Next().ToString();

            Assert.That(host.Split('.'), Has.Length.EqualTo(2));
        }

        [Test]
        public void Hostname_MultipleSubDomains_ReturnsHostname()
        {
            var generator = new HostnameGenerator(5, 5);

            var host = generator.Next().ToString();

            Assert.That(host.Split('.'), Has.Length.EqualTo(6));
        }
    }
}
