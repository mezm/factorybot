using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class ByteRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_NoCondition_ReturnsByte() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Byte = x.Byte.Any() });

        [Test]
        public void Any_InRange_ReturnsByte() => AssertGeneratorValue(x => new AllTypesModel { Byte = x.Byte.Any(10, 150) }, Is.InRange(10, 150));

        [Test]
        public void Any_SingleValue_ReturnsTheValue()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Byte = x.Byte.Any(150, 150) },
                Is.EqualTo(150),
                Is.EqualTo(150));
        }

        [Test]
        public void Any_WrongRange_ThrowsError() => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { Byte = x.Byte.Any(10, 0) });
    }
}