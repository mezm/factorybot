using FactoryBot.Generators.Strings;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class RandomStringFromListGeneratorTest
    {
        [Test]
        public void Generate()
        {
            var list = new[] {"a", "ab", "bc", "def"};
            var generator = new RandomStringFromListGenerator(list);
            var str = (string) generator.Next();
            Assert.That(list, Does.Contain(str));
        }

        [Test]
        public void CreateWithEmptyList()
        {
            Assert.That(() => new RandomStringFromListGenerator(new string[0]), Throws.ArgumentException);
        }
    }
}