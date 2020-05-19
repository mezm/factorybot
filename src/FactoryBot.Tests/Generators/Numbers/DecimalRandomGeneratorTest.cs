using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class DecimalRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_NoCondition_ReturnsDecimal() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Decimal = x.Decimal.Any() });

        [Test]
        public void Any_InRange_ReturnsDecimal()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Decimal = x.Decimal.Any(-1.2m, 5.4457m) },
                Is.InRange(-1.2m, 5.4457m));
        }

        [Test]
        public void Any_SingleValue_ReturnsTheValue()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Decimal = x.Decimal.Any(-1.2m, -1.2m) },
                Is.EqualTo(-1.2m),
                Is.EqualTo(-1.2m));
        }

        [Test]
        public void Any_WrongRange_ThrowsError() => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { Decimal = x.Decimal.Any(10m, -1.005m) });
    }
}