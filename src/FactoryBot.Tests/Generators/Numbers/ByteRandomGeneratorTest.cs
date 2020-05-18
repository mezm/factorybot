using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class ByteRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void GenerateRandom() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Byte = x.Byte.Any() });

        [Test]
        public void GenerateRandomFromRange() => AssertGeneratorValue(x => new AllTypesModel { Byte = x.Byte.Any(10, 150) }, Is.InRange(10, 150));

        [Test]
        public void GenerateRandomSingleValue()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Byte = x.Byte.Any(150, 150) },
                Is.EqualTo(150),
                Is.EqualTo(150));
        }

        [Test]
        public void GenerateRandomWrongRange() => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { Byte = x.Byte.Any(10, 0) });
    }
}