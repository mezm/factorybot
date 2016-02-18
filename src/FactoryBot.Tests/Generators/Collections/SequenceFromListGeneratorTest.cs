using FactoryBot.Generators.Collections;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Collections
{
    [TestFixture]
    public class SequenceFromListGeneratorTest
    {
        [Test]
        public void Generate()
        {
            var list = new[] { "a", "ab", "bc", "def" };
            var generator = new SequenceFromListGenerator<string>(list);

            Assert.That(generator.Next(), Is.EqualTo("a"));
            Assert.That(generator.Next(), Is.EqualTo("ab"));
        }

        [Test]
        public void GenerateLoop()
        {
            var list = new[] { 1.1, -14, 0.4457 };
            var generator = new SequenceFromListGenerator<double>(list);

            Assert.That(generator.Next(), Is.EqualTo(1.1));
            Assert.That(generator.Next(), Is.EqualTo(-14));
            Assert.That(generator.Next(), Is.EqualTo(0.4457));
            Assert.That(generator.Next(), Is.EqualTo(1.1));
        }

        [Test]
        public void CreateWithEmptyList()
        {
            Assert.That(() => new SequenceFromListGenerator<int>(new int[0]), Throws.ArgumentException);
        }
    }
}