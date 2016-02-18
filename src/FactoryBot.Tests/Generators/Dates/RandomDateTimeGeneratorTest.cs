using System;

using FactoryBot.Generators.Dates;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Dates
{
    [TestFixture]
    public class RandomDateTimeGeneratorTest
    {
        [Test]
        public void GenerateRandom()
        {
            var generator = new RandomDateTimeGenerator();

            var n1 = (DateTime)generator.Next();
            var n2 = (DateTime)generator.Next();

            Assert.That(n1, Is.Not.EqualTo(n2));
        }

        [Test]
        public void GenerateRandomFromRange()
        {
            var from = new DateTime(2015, 10, 4);
            var to = new DateTime(2025, 1, 1);
            var generator = new RandomDateTimeGenerator(from, to);

            var n1 = (DateTime)generator.Next();
            var n2 = (DateTime)generator.Next();

            Assert.That(n1, Is.InRange(from, to).And.Not.EqualTo(n2));
            Assert.That(n2, Is.InRange(from, to));
        }

        [Test]
        public void GenerateRandomSingleValue()
        {
            var value = DateTime.Now;
            var generator = new RandomDateTimeGenerator(value, value);

            var n1 = (DateTime)generator.Next();
            var n2 = (DateTime)generator.Next();

            Assert.That(n1, Is.EqualTo(value).And.EqualTo(n2));
        }

        [Test]
        public void GenerateRandomWrongRange()
        {
            Assert.That(
                () => new RandomDateTimeGenerator(new DateTime(2017, 1, 1), new DateTime(1992, 1, 10)),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
    }
}