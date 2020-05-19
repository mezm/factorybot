using System.IO;
using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    // [Timeout(10000)] // todo: Not currently available in the .NET Standard builds of the framework.
    public class SequenceStringFromFileGeneratorTest : GeneratorTestKit
    {
        private const string FILENAME = "text.txt";

        [Test]
        public void SequenceFromFile_FileHasFewLines_ReturnsLinesInOrder()
        {
            var contents = new[] { "this", "is", "the test", "file " };
            File.WriteAllLines(FILENAME, contents);

            FileUtils.WithFileDisposal(
                FILENAME,
                () => AssertGeneratorValue(x => new AllTypesModel { String = x.Strings.SequenceFromFile(FILENAME) },
                    Is.EqualTo("this"),
                    Is.EqualTo("is"),
                    Is.EqualTo("the test")));
        }

        [Test]
        public void SequenceFromFile_RequestValuesMoreThenInSequence_ReturnsLinesInLoop()
        {
            var contents = new[] { "this", "is", "the test", "file " };
            File.WriteAllLines(FILENAME, contents);

            FileUtils.WithFileDisposal(
                FILENAME,
                () => AssertGeneratorValue(x => new AllTypesModel { String = x.Strings.SequenceFromFile(FILENAME) },
                    Is.EqualTo("this"),
                    Is.EqualTo("is"),
                    Is.EqualTo("the test"),
                    Is.EqualTo("file "),
                    Is.EqualTo("this")));
        }

        [Test]
        public void SequenceFromFile_FileHasSingleLine_ReturnsTheLine()
        {
            File.WriteAllLines(FILENAME, new[] { "foo" });

            FileUtils.WithFileDisposal(
                FILENAME,
                () => AssertGeneratorValue(x => new AllTypesModel { String = x.Strings.SequenceFromFile(FILENAME) },
                    Is.EqualTo("foo"),
                    Is.EqualTo("foo")));
        }

        [Test]
        public void SequenceFromFile_FileIsEmpty_ThrowsError()
        {
            File.WriteAllLines(FILENAME, new string[0]);

            FileUtils.WithFileDisposal(
                FILENAME,
                () => ExpectBuildException<IOException>(x => new AllTypesModel { String = x.Strings.SequenceFromFile(FILENAME) }));
        }

        [Test]
        public void SequenceFromFile_FileNotExists_ThrowsError() => ExpectInitException<IOException>(x => new AllTypesModel { String = x.Strings.SequenceFromFile("some_non_existing.aaa") });
    }
}