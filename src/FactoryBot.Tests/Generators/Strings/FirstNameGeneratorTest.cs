using FactoryBot.Extensions;
using FactoryBot.Generators.Strings;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class FirstNameGeneratorTest
    {
        [Test]
        public void GenerateName()
        {
            var generator = new FirstNameGenerator();

            var name1 = (string)generator.Next();
            var name2 = (string)generator.Next();

            Assert.That(name1, Is.Not.Null);
            Assert.That(name2, Is.Not.Null.And.Not.EqualTo(name1));
            Assert.That(name1.Words(), Has.Length.EqualTo(1));
            Assert.That(name2.Words(), Has.Length.EqualTo(1));
        }
    }
}