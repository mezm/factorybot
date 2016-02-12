using System.IO;

using FactoryBot.Generators.Strings;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class RandomLineFromFileGeneratorTest
    {
        [Test]
        public void GetRandomLine()
        {
            const string Filename = "text.txt";
            var contents = new[] { "this", "is", "the test", "file " };
            File.WriteAllLines(Filename, contents);

            try
            {
                var generator = new RandomLineFromFileGenerator(Filename);
                var line = (string)generator.Next();

                Assert.That(line, Is.Not.Null);
                Assert.That(contents, Does.Contain(line));
            }
            finally
            {
                File.Delete(Filename);
            }
        }

        [Test]
        public void CreateWithNonExistingFile()
        {
            Assert.That(
                () => new RandomLineFromFileGenerator("some_non_existing.aaa"),
                Throws.InstanceOf<IOException>());
        }
    }
}