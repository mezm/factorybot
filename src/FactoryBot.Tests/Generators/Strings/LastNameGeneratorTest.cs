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
        public void AlwaysGetNewName() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Strings.LastName() });

        [Test]
        public void GenerateName()
        {
            var source = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.LAST_NAMES);
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Strings.LastName() },
                x =>
                    {
                        Assert.That(x.Words(), Has.Length.EqualTo(1));
                        Assert.That(source, Does.Contain(x));
                    });
        }
    }
}