using System.IO;
using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    // [Timeout(10000)] // todo: Not currently available in the .NET Standard builds of the framework.
    public class RandomLineFromFileGeneratorTest : GeneratorTestKit
    {
        private const string FILENAME = "text.txt";

        [Test]
        public void RandomFromFile_FileHasMultipleLines_ReturnsLineFromFile()
        {
            var content = new[] { "this", "is", "the test", "file " };
            File.WriteAllLines(FILENAME, content);

            FileUtils.WithFileDisposal(
                FILENAME,
                () => AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.RandomFromFile(FILENAME) },
                    x => Assert.That(content, Does.Contain(x))));
        }

        [Test]
        public void RandomFromFile_FileHasSingleLine_ReturnsTheLine()
        {
            File.WriteAllLines(FILENAME, new[] { "the single line test." });

            FileUtils.WithFileDisposal(
                FILENAME,
                () => AssertGeneratorValue(x => new AllTypesModel { String = x.Strings.RandomFromFile(FILENAME) },
                    Is.EqualTo("the single line test."), 
                    Is.EqualTo("the single line test.")));
        }

        [Test]
        public void RandomFromFile_FileIsEmpty_ThrowsError()
        {
            File.WriteAllLines(FILENAME, new string[0]);

            FileUtils.WithFileDisposal(
                FILENAME,
                () => ExpectBuildException<IOException>(x => new AllTypesModel { String = x.Strings.RandomFromFile(FILENAME) }));
        }

        [Test]
        public void RandomFromFile_FileNotExists_ThrowsError() => ExpectInitException<IOException>(x => new AllTypesModel { String = x.Strings.RandomFromFile("some_non_existing.aaa") });
    }
}