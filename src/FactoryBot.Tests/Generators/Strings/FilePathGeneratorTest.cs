using System;
using System.IO;
using System.Reflection;

using FactoryBot.Generators.Strings;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class FilePathGeneratorTest
    {
        private string _folderToRemove;

        [TearDown]
        public void Terminate()
        {
            if (!string.IsNullOrWhiteSpace(_folderToRemove) && Directory.Exists(_folderToRemove))
            {
                Directory.Delete(_folderToRemove, true);
            }
        }

        [Test]
        public void GetNotExistingFileFromEverywhere()
        {
            var generator = new FilePathGenerator();

            var path1 = (string)generator.Next();
            var path2 = (string)generator.Next();

            Assert.That(path1, Is.Not.Null);
            AssertFilePathValidButNotExisting(path1);
            Assert.That(path2, Is.Not.Null.And.Not.EqualTo(path1));
            AssertFilePathValidButNotExisting(path2);
        }

        [Test]
        public void GetNotExistingFileFromFolder()
        {
            var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var generator = new FilePathGenerator(folder);

            var path = (string)generator.Next();

            AssertFilePathValidButNotExisting(path);
            Assert.That(path, Does.StartWith(folder + "\\"));
        }

        [Test]
        public void GetExistingFileFromEverywhere()
        {
            var generator = new FilePathGenerator(existing: true);
            var path = (string)generator.Next();
            AssertFileExists(path);
        }

        [Test]
        public void GetExistingFileFromFolder()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) + "\\";
            var generator = new FilePathGenerator(folder, true);
            var path = (string)generator.Next();
            AssertFileExists(path);
            Assert.That(path, Does.StartWith(folder));
        }

        [Test]
        public void GetExistingFileFromNotExistingFolder()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
            while (Directory.Exists(folder))
            {
                folder += $"\\{new Guid().ToString("D")}";
            }

            Assert.That(() => new FilePathGenerator(folder, true), Throws.InstanceOf<IOException>());
        }

        [Test]
        public void GetExistingFileFromEmptyFolder()
        {
            var directory = new DirectoryInfo(new Guid().ToString("D"));
            directory.Create();
            _folderToRemove = directory.FullName;

            var generator = new FilePathGenerator(directory.FullName, true);
            Assert.That(() => generator.Next(), Throws.InstanceOf<IOException>());
        }

        private static void AssertFilePathValidButNotExisting(string path)
        {
            Assert.That(Path.IsPathRooted(path), "Path is incorrect.");
            Assert.That(Path.GetFileName(path), Is.Not.Empty, "File name is absent.");
        }

        private static void AssertFileExists(string path)
        {
            Assert.That(Path.GetFileName(path), Is.Not.Empty, "File name is absent");
            Assert.That(path, Does.Exist);
        }
    }
}