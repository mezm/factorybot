using FactoryBot.Extensions;
using NUnit.Framework;

namespace FactoryBot.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTest
    {
        public const string MULTI_LINE_EMPTY_INPUT = @"



";
        public const string MULTI_LINE_SINGLE_BREAK_INPUT = @"wicked workers 
proceed no further";

        public const string MULTI_LINE_FEW_BREAKS_INPUT = @"
wicked
workers

proceed no further";

        [Test]
        public void Words_FromEmptyString_ReturnsEmptyArray() => Assert.That("".Words(), Is.EqualTo(new string[0]));

        [Test]
        public void Words_OnlyOne_ReturnsOneWord() => Assert.That("word".Words(), Is.EqualTo(new[] { "word" }));

        [Test]
        public void Words_Few_ReturnsWords()
        {
            Assert.That(
                "As, to be clothed in purple, to drink in gold, a".Words(), 
                Is.EqualTo(new[] { "As", "to", "be", "clothed", "in", "purple", "to", "drink", "in", "gold", "a" }));
        }

        [Test]
        public void Words_WithHyphen_ReturnsWords()
        {
            Assert.That(
                "As-to be clothed in purple, to-drink-in gold, and".Words(),
                Is.EqualTo(new[] { "As-to", "be", "clothed", "in", "purple", "to-drink-in", "gold", "and" }));
        }

        [Test]
        public void Words_NoWords_ReturnsEmptyArray() => Assert.That(",! #-# ---".Words(), Is.EquivalentTo(new string[0]));

        [Test]
        [TestCase("", "")]
        [TestCase("word", "word")]
        [TestCase("wicked workers proceed no further", "wicked workers proceed no further")]
        [TestCase(MULTI_LINE_EMPTY_INPUT, "")]
        [TestCase(MULTI_LINE_SINGLE_BREAK_INPUT, "wicked workers proceed no further")]
        [TestCase(MULTI_LINE_FEW_BREAKS_INPUT, "wicked workers proceed no further")]
        public void RemoveLineBreaks_TestCase_ReturnsStringWithoutBreaks(string str, string expected) => Assert.That(str.RemoveLineBreaks(), Is.EqualTo(expected));
    }
}