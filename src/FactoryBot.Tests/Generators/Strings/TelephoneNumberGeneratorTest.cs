using FactoryBot.Generators.Strings;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class TelephoneNumberGeneratorTest
    {
        [Test]
        public void GenerateDefaultNumber()
        {
            var generator = new TelephoneNumberGenerator();

            var telephone1 = (string) generator.Next();
            var telephone2 = (string) generator.Next();

            Assert.That(telephone1, Is.Not.Null);
            Assert.That(telephone2, Is.Not.Null.And.Not.EqualTo(telephone1));
        }

        [Test]
        public void GenerateNumberFromTemplate()
        {
            var generator = new TelephoneNumberGenerator("+1-800-###-####");
            var telephone = (string)generator.Next();
            Assert.That(telephone, Does.Match(@"\+1\-800\-\d{3}\-\d{4}"));
        }

        [Test]
        public void GenerateNumberFromTemplateWithoutRandomData()
        {
            var generator = new TelephoneNumberGenerator("+4-805-111-22");

            var telephone1 = (string)generator.Next();
            var telephone2 = (string)generator.Next();

            Assert.That(telephone1, Is.EqualTo("+4-805-111-22"));
            Assert.That(telephone2, Is.EqualTo("+4-805-111-22"));
        }
    }
}