using FactoryBot.Generators.Collections;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Collections
{
    [TestFixture]
    public class RandomFromListGeneratorTest
    {
        [Test]
        public void Generate()
        {
            var list = new[] {"a", "ab", "bc", "def"};
            var generator = new RandomFromListGenerator<string>(list);
            var str = (string) generator.Next();
            Assert.That(list, Does.Contain(str));
        }

        [Test]
        public void CreateWithEmptyList()
        {
            Assert.That(() => new RandomFromListGenerator<decimal>(new decimal[0]), Throws.ArgumentException);
        }
    }
}