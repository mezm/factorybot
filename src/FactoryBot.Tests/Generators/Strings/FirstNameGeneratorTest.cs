using FactoryBot.Extensions;
using FactoryBot.Generators;
using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class FirstNameGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void FirstName_NoCondition_ReturnsNewName() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Names.FirstName() });

        [Test]
        public void FirstName_NoCondition_ReturnsNameFromTheSource()
        {
            var source = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.FIRST_NAMES);
            AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Names.FirstName() },
                x =>
                    {
                        Assert.That(x.Words(), Has.Length.EqualTo(1));
                        Assert.That(source, Does.Contain(x));
                    });
        }
    }
}