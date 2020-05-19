using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class LongRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_NoCondition_ReturnsLong() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Long = x.Long.Any() });

        [Test]
        public void Any_InRange_ReturnsLong()
        {
            AssertGeneratorValue(x => new AllTypesModel { Long = x.Long.Any(100000000000000000, 100000000000000050) }, Is.InRange(100000000000000000, 100000000000000050));
        }

        [Test]
        public void Any_SingleValue_ReturnsTheValue()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Long = x.Long.Any(100000000000000050, 100000000000000050) },
                Is.EqualTo(100000000000000050),
                Is.EqualTo(100000000000000050));
        }

        [Test]
        public void Any_WrongRange_ThrowsError() => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { Long = x.Long.Any(10, -10) });
    }
}