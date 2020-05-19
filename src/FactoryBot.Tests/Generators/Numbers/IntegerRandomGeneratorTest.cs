using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class IntegerRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_NoCondition_ReturnsInteger() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Integer = x.Integer.Any() });

        [Test]
        public void Any_InRange_ReturnsInteger() => AssertGeneratorValue(x => new AllTypesModel { Integer = x.Integer.Any(10, 150) }, Is.InRange(10, 150));

        [Test]
        public void Any_SingleValue_ReturnsTheValue()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Integer = x.Integer.Any(150, 150) },
                Is.EqualTo(150),
                Is.EqualTo(150));
        }

        [Test]
        public void Any_WrongRange_ThrowsError() => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { Integer = x.Integer.Any(10, -10) });
    }
}