using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class DoubleRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_NoCondition_ReturnsDouble() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Double = x.Double.Any() });

        [Test]
        public void Any_InRange_ReturnsDouble()
        {
            AssertGeneratorValue(x => new AllTypesModel { Double = x.Double.Any(-1.2, 5.4457) }, Is.InRange(-1.2, 5.4457));
        }

        [Test]
        public void Any_SingleValue_ReturnsTheValue()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Double = x.Double.Any(-1.2, -1.2) },
                Is.EqualTo(-1.2),
                Is.EqualTo(-1.2));
        }

        [Test]
        public void Any_WrongRange_ThrowsError() => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { Double = x.Double.Any(10, -1.005) });
    }
}