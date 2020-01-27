using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class PhoneNumberGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void GenerateDefaultNumber() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Address.PhoneNumber() });

        [Test]
        public void GenerateNumberFromTemplate() => AssertGeneratorValue(x => new AllTypesModel { String = x.Address.PhoneNumber("+1-800-###-####") }, Does.Match(@"\+1\-800\-\d{3}\-\d{4}"));

        [Test]
        public void GenerateNumberFromTemplateWithoutRandomData()
        {
            AssertGeneratorValue(x => new AllTypesModel { String = x.Address.PhoneNumber("+4-805-111-22") },
                Is.EqualTo("+4-805-111-22"), 
                Is.EqualTo("+4-805-111-22"));
        }
    }
}