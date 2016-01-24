using FactoryBot.Extensions;

using NUnit.Framework;

namespace FactoryBot.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [Test]
        public void GetWordsFromEmptyString()
        {
            Assert.That("".Words(), Is.EqualTo(new string[0]));
        }

        [Test]
        public void GetWordsOnlyOne()
        {
            Assert.That("word".Words(), Is.EqualTo(new[] { "word" }));
        }

        [Test]
        public void GetWordsFew()
        {
            Assert.That(
                "As, to be clothed in purple, to drink in gold, a".Words(), 
                Is.EqualTo(new[] { "As", "to", "be", "clothed", "in", "purple", "to", "drink", "in", "gold", "a" }));
        }

        [Test]
        public void GetWordsWithHyphen()
        {
            Assert.That(
                "As-to be clothed in purple, to-drink-in gold, and".Words(),
                Is.EqualTo(new[] { "As-to", "be", "clothed", "in", "purple", "to-drink-in", "gold", "and" }));
        }

        [Test]
        public void GetWordsNoWords()
        {
            Assert.That(",! #-# ---".Words(), Is.EquivalentTo(new string[0]));
        }
    }
}