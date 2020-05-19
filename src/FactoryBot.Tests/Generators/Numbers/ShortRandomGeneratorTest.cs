using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class ShortRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_NoCondition_ReturnsShort() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Short = x.Short.Any() });

        [Test]
        public void Any_InRange_ReturnsShort() => AssertGeneratorValue(x => new AllTypesModel { Short = x.Short.Any(10, 150) }, Is.InRange(10, 150));

        [Test]
        public void Any_SingleValue_ReturnsTheValue()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Short = x.Short.Any(150, 150) },
                Is.EqualTo(150),
                Is.EqualTo(150));
        }

        [Test]
        public void Any_WrongRange_ThrowsError() => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { Short = x.Short.Any(10, -10) });
    }
}