using System.IO;
using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture, Timeout(10000)]
    public class SequenceStringFromFileGeneratorTest : GeneratorTestKit
    {
        private const string Filename = "text.txt";

        [Test]
        public void GetNextLine()
        {
            var contents = new[] { "this", "is", "the test", "file " };
            File.WriteAllLines(Filename, contents);

            FileUtils.WithFileDisposal(
                Filename,
                () => AssertGeneratorValue(x => new AllTypesModel { String = x.Strings.SequenceFromFile(Filename) },
                    Is.EqualTo("this"),
                    Is.EqualTo("is"),
                    Is.EqualTo("the test")));
        }

        [Test]
        public void LoopValues()
        {
            var contents = new[] { "this", "is", "the test", "file " };
            File.WriteAllLines(Filename, contents);

            FileUtils.WithFileDisposal(
                Filename,
                () => AssertGeneratorValue(x => new AllTypesModel { String = x.Strings.SequenceFromFile(Filename) },
                    Is.EqualTo("this"),
                    Is.EqualTo("is"),
                    Is.EqualTo("the test"),
                    Is.EqualTo("file "),
                    Is.EqualTo("this")));
        }

        [Test]
        public void GenerateFromFileWithSingleLine()
        {
            File.WriteAllLines(Filename, new[] { "foo" });

            FileUtils.WithFileDisposal(
                Filename,
                () => AssertGeneratorValue(x => new AllTypesModel { String = x.Strings.SequenceFromFile(Filename) },
                    Is.EqualTo("foo"),
                    Is.EqualTo("foo")));
        }

        [Test]
        public void GenerateFromEmptyFile()
        {
            File.WriteAllLines(Filename, new string[0]);

            FileUtils.WithFileDisposal(
                Filename,
                () => ExpectBuildException<IOException>(x => new AllTypesModel { String = x.Strings.SequenceFromFile(Filename) }));
        }

        [Test]
        public void CreateWithNonExistingFile()
        {
            ExpectInitException<IOException>(x => new AllTypesModel { String = x.Strings.SequenceFromFile("some_non_existing.aaa") });
        }
    }
}