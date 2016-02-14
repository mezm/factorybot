using System;

using FactoryBot.Generators.Numbers;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class IntegerRandomGeneratorTest
    {
        [Test]
        public void GenerateRandom()
        {
            var generator = new IntegerRandomGenerator();

            var n1 = (int)generator.Next();
            var n2 = (int)generator.Next();

            Assert.That(n1, Is.Not.EqualTo(n2));
        }

        [Test]
        public void GenerateRandomFromRange()
        {
            var generator = new IntegerRandomGenerator(10, 150);

            var n1 = (int)generator.Next();
            var n2 = (int)generator.Next();

            Assert.That(n1, Is.InRange(10, 150).And.Not.EqualTo(n2));
            Assert.That(n2, Is.InRange(10, 150));
        }

        [Test]
        public void GenerateRandomSingleValue()
        {
            var generator = new IntegerRandomGenerator(150, 150);

            var n1 = (int)generator.Next();
            var n2 = (int)generator.Next();

            Assert.That(n1, Is.EqualTo(150).And.EqualTo(n2));
        }

        [Test]
        public void GenerateRandomWrongRange()
        {
            Assert.That(() => new IntegerRandomGenerator(10, -10), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
    }
}