using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Collections
{
    [TestFixture]
    public class SequenceFromListGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Generate()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { String = x.Strings.SequenceFromList(new[] { "a", "ab", "bc", "def" }) },
                Is.EqualTo("a"),
                Is.EqualTo("ab"));
        }

        [Test]
        public void GenerateLoop()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Double = x.Double.SequenceFromList(new[] { 1.1, -14, 0.4457 }) },
                Is.EqualTo(1.1),
                Is.EqualTo(-14),
                Is.EqualTo(0.4457),
                Is.EqualTo(1.1));
        }

        [Test]
        public void CreateWithEmptyList()
        {
            ExpectArgumentInitException(x => new AllTypesModel { Integer = x.Integer.SequenceFromList(new int[0]) });
        }
    }
}