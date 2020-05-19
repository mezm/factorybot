using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class FloatRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_NoCondition_ReturnsFloat() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Float = x.Float.Any() });

        [Test]
        public void Any_InRange_ReturnsFloat()
        {
            AssertGeneratorValue(x => new AllTypesModel { Float = x.Float.Any(-1.2f, 5.4457f) }, Is.InRange(-1.2f, 5.4457f));
        }

        [Test]
        public void Any_SingleValue_ReturnsTheValue()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Float = x.Float.Any(-1.2f, -1.2f) },
                Is.EqualTo(-1.2f),
                Is.EqualTo(-1.2f));
        }

        [Test]
        public void Any_WrongRange_ThrowsError() => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { Float = x.Float.Any(10f, -1.005f) });
    }
}