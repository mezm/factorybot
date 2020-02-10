using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class LongRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void GenerateRandom() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Long = x.Long.Any() });

        [Test]
        public void GenerateRandomFromRange()
        {
            AssertGeneratorValue(x => new AllTypesModel { Long = x.Long.Any(100000000000000000, 100000000000000050) }, Is.InRange(100000000000000000, 100000000000000050));
        }

        [Test]
        public void GenerateRandomSingleValue()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Long = x.Long.Any(100000000000000050, 100000000000000050) },
                Is.EqualTo(100000000000000050),
                Is.EqualTo(100000000000000050));
        }

        [Test]
        public void GenerateRandomWrongRange() => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { Long = x.Long.Any(10, -10) });
    }
}