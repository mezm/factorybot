using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class FloatRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void GetRandom() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Float = x.Float.Any() });

        [Test]
        public void GetRandomWithThreshfold()
        {
            AssertGeneratorValue(x => new AllTypesModel { Float = x.Float.Any(-1.2f, 5.4457f) }, Is.InRange(-1.2f, 5.4457f));
        }

        [Test]
        public void GetRandomWithThreshfoldSingleValue()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Float = x.Float.Any(-1.2f, -1.2f) },
                Is.EqualTo(-1.2f),
                Is.EqualTo(-1.2f));
        }

        [Test]
        public void CreateWithWrongRange() => ExpectArgumentOutOfRangeInitException(x => new AllTypesModel { Float = x.Float.Any(10f, -1.005f) });
    }
}