using FactoryBot.Extensions;

using NUnit.Framework;

namespace FactoryBot.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTest
    {
        public const string MultiLineEmptyInput = @"



";
        public const string MultiLineSingleBreakInput = @"wicked workers 
proceed no further";

        public const string MultiLineFewBreaksInput = @"
wicked
workers

proceed no further";

        [Test]
        public void GetWordsFromEmptyString() => Assert.That("".Words(), Is.EqualTo(new string[0]));

        [Test]
        public void GetWordsOnlyOne() => Assert.That("word".Words(), Is.EqualTo(new[] { "word" }));

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
        public void GetWordsNoWords() => Assert.That(",! #-# ---".Words(), Is.EquivalentTo(new string[0]));

        [Test]
        [TestCase("", "")]
        [TestCase("word", "word")]
        [TestCase("wicked workers proceed no further", "wicked workers proceed no further")]
        [TestCase(MultiLineEmptyInput, "")]
        [TestCase(MultiLineSingleBreakInput, "wicked workers proceed no further")]
        [TestCase(MultiLineFewBreaksInput, "wicked workers proceed no further")]
        public void RemoveLineBreaks(string str, string expected) => Assert.That(str.RemoveLineBreaks(), Is.EqualTo(expected));
    }
}