using System;
using FactoryBot.Generators;
using FactoryBot.Generators.Strings;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class StringRandomGeneratorTest
    {
        private string _fileContent;

        [SetUp]
        public void Init()
        {
            _fileContent = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.RandomText);
        }

        [Test]
        public void GenerateNextWithoutThreshold()
        {
            var generator = new StringRandomGenerator();

            var str1 = (string)generator.Next();
            var str2 = (string)generator.Next();

            Assert.That(str1, Is.Not.Null);
            Assert.That(str2, Is.Not.Null.And.Not.EqualTo(str1));
            Assert.That(_fileContent, Does.Contain(str1).And.Contain(str2));
        }

        [Test]
        public void GenerateNextWithThreshold()
        {
            var generator = new StringRandomGenerator(3, 15);

            var str1 = (string)generator.Next();
            var str2 = (string)generator.Next();

            Assert.That(str1, Is.Not.Null);
            Assert.That(str2, Is.Not.Null.And.Not.EqualTo(str1));
            Assert.That(str1, Has.Length.InRange(3, 15));
            Assert.That(str2, Has.Length.InRange(3, 15));
            Assert.That(_fileContent, Does.Contain(str1).And.Contain(str2));
        }

        [Test]
        public void GenerateNextWithConstantThreshold()
        {
            var generator = new StringRandomGenerator(10, 10);

            var str1 = (string)generator.Next();
            var str2 = (string)generator.Next();
            var str3 = (string)generator.Next();

            Assert.That(str1, Has.Length.EqualTo(10));
            Assert.That(str2, Has.Length.EqualTo(10));
            Assert.That(str3, Has.Length.EqualTo(10));
        }

        [Test]
        [TestCase(-10, 40)]
        [TestCase(10, -40)]
        [TestCase(40, 10)]
        [TestCase(0, 0)]
        public void CreateWithInvalidThreshold(int min, int max)
        {
            Assert.That(() => new StringRandomGenerator(min, max), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}