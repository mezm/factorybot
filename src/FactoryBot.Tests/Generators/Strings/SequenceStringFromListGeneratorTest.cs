using FactoryBot.Generators.Strings;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class SequenceStringFromListGeneratorTest
    {
        [Test]
        public void Generate()
        {
            var list = new[] { "a", "ab", "bc", "def" };
            var generator = new SequenceStringFromListGenerator(list);

            Assert.That(generator.Next(), Is.EqualTo("a"));
            Assert.That(generator.Next(), Is.EqualTo("ab"));
        }

        [Test]
        public void GenerateLoop()
        {
            var list = new[] { "a", "ab", "def" };
            var generator = new SequenceStringFromListGenerator(list);

            Assert.That(generator.Next(), Is.EqualTo("a"));
            Assert.That(generator.Next(), Is.EqualTo("ab"));
            Assert.That(generator.Next(), Is.EqualTo("def"));
            Assert.That(generator.Next(), Is.EqualTo("a"));
        }

        [Test]
        public void CreateWithEmptyList()
        {
            Assert.That(() => new SequenceStringFromListGenerator(new string[0]), Throws.ArgumentException);
        }
    }
}