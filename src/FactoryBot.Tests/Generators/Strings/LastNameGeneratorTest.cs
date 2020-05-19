using FactoryBot.Extensions;
using FactoryBot.Generators;
using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class LastNameGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void LastName_NoCondition_ReturnsNewName() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Names.LastName() });

        [Test]
        public void LastName_NoCondition_ReturnsNamesFromTheSource()
        {
            var source = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.LAST_NAMES);
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Names.LastName() },
                x =>
                    {
                        Assert.That(x.Words(), Has.Length.EqualTo(1));
                        Assert.That(source, Does.Contain(x));
                    });
        }
    }
}