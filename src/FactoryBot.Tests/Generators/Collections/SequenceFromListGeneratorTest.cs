using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Collections
{
    [TestFixture]
    public class SequenceFromListGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void SequenceFromList_NonEmptySequence_ReturnsValuesInOrder()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { String = x.Strings.SequenceFromList("a", "ab", "bc", "def") },
                Is.EqualTo("a"),
                Is.EqualTo("ab"));
        }

        [Test]
        public void SequenceFromList_BuildMoreThenSequenceLength_ReturnsSequenceInLoop()
        {
            AssertGeneratorValue(
                x => new AllTypesModel { Double = x.Double.SequenceFromList(1.1, -14, 0.4457) },
                Is.EqualTo(1.1),
                Is.EqualTo(-14),
                Is.EqualTo(0.4457),
                Is.EqualTo(1.1));
        }

        [Test]
        public void SequenceFromList_EmptyList_ThrowsError() => ExpectArgumentInitException(x => new AllTypesModel { Integer = x.Integer.SequenceFromList() });
    }
}