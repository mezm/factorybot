using FactoryBot.Extensions;
using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    public class WordRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Words_NoCondition_ReturnsNewWords() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Strings.Words() });

        [Test]
        public void Words_CountInRange_ReturnsWords()
        {
            AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.Words(3, 15) },
                x => Assert.That(x.Words(), Has.Length.InRange(3, 15)));
        }

        [Test]
        public void Words_CountIsStatic_ReturnsGivenNumberOfWords()
        {
            AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.Words(10, 10) },
                x => Assert.That(x.Words(), Has.Length.EqualTo(10)));
        }

        [Test]
        [TestCase(-10, 40)]
        [TestCase(10, -40)]
        [TestCase(40, 10)]
        [TestCase(0, 0)]
        public void Words_CountRangeIsIncorrect_ThrowsError(int min, int max) => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { String = x.Strings.Words(min, max) });
    }
}