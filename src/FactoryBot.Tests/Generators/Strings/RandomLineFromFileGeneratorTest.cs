using System.IO;
using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    // [Timeout(10000)] // todo: Not currently available in the .NET Standard builds of the framework.
    public class RandomLineFromFileGeneratorTest : GeneratorTestKit
    {
        private const string Filename = "text.txt";

        [Test]
        public void GetRandomLine()
        {
            var content = new[] { "this", "is", "the test", "file " };
            File.WriteAllLines(Filename, content);

            FileUtils.WithFileDisposal(
                Filename,
                () => AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.RandomFromFile(Filename) },
                    x => Assert.That(content, Does.Contain(x))));
        }

        [Test]
        public void GenerateFromFileWithSingleLine()
        {
            File.WriteAllLines(Filename, new[] { "the single line test." });

            FileUtils.WithFileDisposal(
                Filename,
                () => AssertGeneratorValue(x => new AllTypesModel { String = x.Strings.RandomFromFile(Filename) },
                    Is.EqualTo("the single line test."), 
                    Is.EqualTo("the single line test.")));
        }

        [Test]
        public void GenerateFromEmptyFile()
        {
            File.WriteAllLines(Filename, new string[0]);

            FileUtils.WithFileDisposal(
                Filename,
                () => ExpectBuildException<IOException>(x => new AllTypesModel { String = x.Strings.RandomFromFile(Filename) }));
        }

        [Test]
        public void CreateWithNonExistingFile() => ExpectInitException<IOException>(x => new AllTypesModel { String = x.Strings.RandomFromFile("some_non_existing.aaa") });
    }
}