using System.IO;

using FactoryBot.Generators.Strings;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture, Timeout(10000)]
    public class SequenceStringFromFileGeneratorTest
    {
        [Test]
        public void GetNextLine()
        {
            const string Filename = "text.txt";
            var contents = new[] { "this", "is", "the test", "file " };
            File.WriteAllLines(Filename, contents);

            FileUtils.WithFileDisposal(
                Filename,
                () =>
                    {
                        var generator = new SequenceStringFromFileGenerator(Filename);
                        var line1 = (string)generator.Next();
                        var line2 = (string)generator.Next();
                        var line3 = (string)generator.Next();

                        Assert.That(line1, Is.EqualTo(contents[0]));
                        Assert.That(line2, Is.EqualTo(contents[1]));
                        Assert.That(line3, Is.EqualTo(contents[2]));
                    });
        }

        [Test]
        public void GenerateFromFileWithSingleLine()
        {
            const string Filename = "text.txt";
            File.WriteAllLines(Filename, new[] { "foo" });

            FileUtils.WithFileDisposal(
                Filename,
                () =>
                {
                    var generator = new SequenceStringFromFileGenerator(Filename);
                    var line1 = (string)generator.Next();
                    var line2 = (string)generator.Next();

                    Assert.That(line1, Is.EqualTo("foo"));
                    Assert.That(line2, Is.EqualTo("foo"));
                });
        }

        [Test]
        public void GenerateFromEmptyFile()
        {
            const string Filename = "text.txt";
            File.WriteAllLines(Filename, new string[0]);

            FileUtils.WithFileDisposal(
                Filename,
                () =>
                {
                    var generator = new SequenceStringFromFileGenerator(Filename);
                    Assert.That(() => generator.Next(), Throws.InstanceOf<IOException>());
                });
        }

        [Test]
        public void CreateWithNonExistingFile()
        {
            Assert.That(
                () => new SequenceStringFromFileGenerator("some_non_existing.aaa"),
                Throws.InstanceOf<IOException>());
        }
    }
}