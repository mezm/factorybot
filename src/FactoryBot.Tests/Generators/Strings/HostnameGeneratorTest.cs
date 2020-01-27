using FactoryBot.Generators.Strings;
using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class HostnameGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void AlwaysNewHostname() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Network.Hostname() });

        [Test]
        public void SingleSubdomain() 
        {
            var generator = new HostnameGenerator(1, 1);

            var host = generator.Next().ToString();

            Assert.That(host.Split('.'), Has.Length.EqualTo(2));
        }

        [Test]
        public void MultipleSubDomains()
        {
            var generator = new HostnameGenerator(5, 5);

            var host = generator.Next().ToString();

            Assert.That(host.Split('.'), Has.Length.EqualTo(6));
        }
    }
}
