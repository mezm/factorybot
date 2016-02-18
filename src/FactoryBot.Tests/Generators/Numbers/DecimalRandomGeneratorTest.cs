using System;

using FactoryBot.Generators.Numbers;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class DecimalRandomGeneratorTest
    {
        [Test]
        public void GetRandom()
        {
            var generator = new DecimalRandomGenerator();

            var d1 = (decimal)generator.Next();
            var d2 = (decimal)generator.Next();

            Assert.That(d1, Is.Not.EqualTo(d2));
        }

        [Test]
        public void GetRandomWithThreshfold()
        {
            var generator = new DecimalRandomGenerator(-1.2m, 5.4457m);
            var d = (decimal)generator.Next();
            Assert.That(d, Is.InRange(-1.2m, 5.4457m));
        }

        [Test]
        public void GetRandomWithThreshfoldSingleValue()
        {
            var generator = new DecimalRandomGenerator(-1.2m, -1.2m);

            var d1 = (decimal)generator.Next();
            var d2 = (decimal)generator.Next();

            Assert.That(d1, Is.EqualTo(-1.2m).And.EqualTo(d2));
        }

        [Test]
        public void CreateWithWrongRange()
        {
            Assert.That(() => new DecimalRandomGenerator(10m, -1.005m), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
    }
}