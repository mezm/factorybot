using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class AddressGeneratorsTest : GeneratorTestKit
    {
        [Test]
        public void NextCountry_NoCondition_Success()
        {
            AssertGeneratorValue(x => new AllTypesModel { String = x.Strings.Address.Country() }, Is.Not.Null);
        }
    }
}