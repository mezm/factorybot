using System;
using System.IO;
using System.Reflection;

using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class FilePathGeneratorTest : GeneratorTestKit
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
        public void GetAlwaysNewRandomFilename() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Strings.Filename() });

        [Test]
        public void GetNotExistingFileFromEverywhere()
        {
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Strings.Filename() },
                AssertFilePathValidButNotExisting);
        }

        [Test]
        public void GetNotExistingFileFromFolder()
        {
            var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Strings.Filename(folder, false) },
                x =>
                    {
                        AssertFilePathValidButNotExisting(x);
                        Assert.That(x, Does.StartWith(folder + "\\"));
                    });
        }

        [Test]
        public void GetExistingFileFromEverywhere()
        {
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Strings.Filename(null, true) },
                AssertFileExists);
        }

        [Test]
        public void GetExistingFileFromFolder()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) + "\\";
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Strings.Filename(folder, true) },
                x =>
                    {
                        AssertFileExists(x);
                        Assert.That(x, Does.StartWith(folder));
                    });
        }

        [Test]
        public void GetExistingFileFromNotExistingFolder()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
            while (Directory.Exists(folder))
            {
                folder += $"\\{new Guid().ToString("D")}";
            }

            ExpectInitException<IOException>(x => new AllTypesModel { String = x.Strings.Filename(folder, true) });
        }

        [Test]
        public void GetExistingFileFromEmptyFolder()
        {
            var directory = new DirectoryInfo(new Guid().ToString("D"));
            directory.Create();
            _folderToRemove = directory.FullName;

            ExpectBuildException<IOException>(x => new AllTypesModel {String = x.Strings.Filename(directory.FullName, true)});
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