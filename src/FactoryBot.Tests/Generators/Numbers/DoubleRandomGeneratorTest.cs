using System;
using FactoryBot.Generators.Numbers;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class DoubleRandomGeneratorTest
    {
        [Test]
        public void GetRandom()
        {
            var generator = new DoubleRandomGenerator();

            var d1 = (double) generator.Next();
            var d2 = (double) generator.Next();

            Assert.That(d1, Is.Not.EqualTo(d2));
        }

        [Test]
        public void GetRandomWithThreshfold()
        {
            var generator = new DoubleRandomGenerator(-1.2, 5.4457);
            var d = (double)generator.Next();
            Assert.That(d, Is.InRange(-1.2, 5.4457));
        }

        [Test]
        public void GetRandomWithThreshfoldSingleValue()
        {
            var generator = new DoubleRandomGenerator(-1.2, -1.2);

            var d1 = (double)generator.Next();
            var d2 = (double)generator.Next();

            Assert.That(d1, Is.EqualTo(-1.2).And.EqualTo(d2));
        }

        [Test]
        public void CreateWithWrongRange()
        {
            Assert.That(() => new DoubleRandomGenerator(10, -1.005), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
    }
}