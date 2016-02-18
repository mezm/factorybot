﻿using System.IO;

using FactoryBot.Generators.Strings;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture, Timeout(10000)]
    public class RandomLineFromFileGeneratorTest
    {
        [Test]
        public void GetRandomLine()
        {
            const string Filename = "text.txt";
            var contents = new[] { "this", "is", "the test", "file " };
            File.WriteAllLines(Filename, contents);

            FileUtils.WithFileDisposal(
                Filename,
                () =>
                {
                    var generator = new RandomLineFromFileGenerator(Filename);
                    var line = (string)generator.Next();

                    Assert.That(line, Is.Not.Null);
                    Assert.That(contents, Does.Contain(line));
                });
        }

        [Test]
        public void GenerateFromFileWithSingleLine()
        {
            const string Filename = "text.txt";
            File.WriteAllLines(Filename, new[] { "the single line test." });

            FileUtils.WithFileDisposal(
                Filename,
                () =>
                {
                    var generator = new RandomLineFromFileGenerator(Filename);
                    var line1 = (string)generator.Next();
                    var line2 = (string)generator.Next();

                    Assert.That(line1, Is.EqualTo("the single line test."));
                    Assert.That(line2, Is.EqualTo("the single line test."));
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
                        var generator = new RandomLineFromFileGenerator(Filename);
                        Assert.That(() => generator.Next(), Throws.InstanceOf<IOException>());
                    });
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