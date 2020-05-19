using FactoryBot.Generators;
using FactoryBot.Tests.Models;
using FactoryBot.Extensions;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class TopLevelDomainGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void TopLevelDomain_NoCondition_ReturnsNewDomain() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Network.TopLevelDomain() });

        [Test]
        public void TopLevelDomain_NoCondition_ReturnsDomainFromTheSource()
        {
            var source = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.TOP_LEVEL_DOMAINS);
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Network.TopLevelDomain() },
                x =>
                {
                    Assert.That(x.Words(), Has.Length.EqualTo(1));
                    Assert.That(source, Does.Contain(x));
                });
        }
    }
}
