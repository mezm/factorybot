using FactoryBot.Generators;
using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class StringRandomGeneratorTest : GeneratorTestKit
    {
        private readonly string _fileContent = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.RANDOM_TEXT);

        [Test]
        public void AlwaysNewValue() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Strings.Any() });

        [Test]
        public void GenerateNextWithoutThreshold() => AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.Any() }, x => Assert.That(_fileContent, Does.Contain(x)));

        [Test]
        public void GenerateNextWithThreshold()
        {
            AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.Any(3, 15) }, x =>
            {
                Assert.That(x, Has.Length.InRange(3, 15));
                Assert.That(_fileContent, Does.Contain(x));
            });
        }

        [Test]
        public void GenerateNextWithConstantThreshold()
        {
            AssertGeneratorValue(x => new AllTypesModel { String = x.Strings.Any(10, 10) }, 
                Has.Length.EqualTo(10),
                Has.Length.EqualTo(10));
        }

        [Test]
        [TestCase(-10, 40)]
        [TestCase(10, -40)]
        [TestCase(40, 10)]
        [TestCase(0, 0)]
        public void CreateWithInvalidThreshold(int min, int max) => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { String = x.Strings.Any(min, max) });
    }
}