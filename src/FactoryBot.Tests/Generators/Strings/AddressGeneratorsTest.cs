using FactoryBot.Generators;
using FactoryBot.Generators.Strings;
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
            var source = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.COUNTRIES);
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Address.Country() }, 
                x => Assert.That(source, Does.Contain(x)));
        }

        [Test]
        public void NextCity_NoCondition_Success()
        {
            var source = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.CITIES);
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Address.City() }, 
                x => Assert.That(source, Does.Contain(x)));
        }

        [Test]
        public void NextState_NoCondition_Success()
        {
            var source = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.STATES);
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Address.State() }, 
                x => Assert.That(source, Does.Contain(x)));
        }

        [Test]
        public void NextPostalCode_Zip_Success()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { String = x.Address.PostalCode(PostalCodeFormat.Zip) }, 
                Is.Not.Null.And.Match(@"^\d{5}$"));
        }

        [Test]
        public void NextStreetAndBuilding_NoCondition_Success()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { String = x.Address.StreetAndBuilding() },
                Is.Not.Null.And.Match(@"^\d+\s[\w\s]+\sst\.$"));
        }
    }
}