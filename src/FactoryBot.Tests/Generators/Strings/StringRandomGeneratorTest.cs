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
        public void Any_NoCondition_RetrnsNewString() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Strings.Any() });

        [Test]
        public void Any_NoCondition_ReturnsStringFromTheSource()
        {
            AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.Any() }, x => Assert.That(_fileContent, Does.Contain(x)));
        }

        [Test]
        public void Any_WithLengthRange_ReturnsString()
        {
            AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.Any(3, 15) }, x =>
            {
                Assert.That(x, Has.Length.InRange(3, 15));
                Assert.That(_fileContent, Does.Contain(x));
            });
        }

        [Test]
        public void Any_WithConstLength_ReturnsString()
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
        public void Any_WrongLengthRange_ThrowsError(int min, int max) => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { String = x.Strings.Any(min, max) });

        [Test]
        public void Any_NonAnsciiChar_ShouldNotExists()
        {
            AssertGeneratorValue(x => new AllTypesModel { String = x.Strings.Any(1_000, 1_000) }, Does.Not.Match("[^\x00-\x7F]+"));
        }
    }
}