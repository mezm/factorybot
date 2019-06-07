using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class DoubleRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void GetRandom() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Double = x.Double.Any() });

        [Test]
        public void GetRandomWithThreshfold()
        {
            AssertGeneratorValue(x => new AllTypesModel { Double = x.Double.Any(-1.2, 5.4457) }, Is.InRange(-1.2, 5.4457));
        }

        [Test]
        public void GetRandomWithThreshfoldSingleValue()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Double = x.Double.Any(-1.2, -1.2) },
                Is.EqualTo(-1.2),
                Is.EqualTo(-1.2));
        }

        [Test]
        public void CreateWithWrongRange() => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { Double = x.Double.Any(10, -1.005) });
    }
}