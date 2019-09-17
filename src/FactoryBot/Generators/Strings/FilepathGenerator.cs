using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FactoryBot.Generators.Strings
{
    public class FilePathGenerator : TypedGenerator<string>
    {
        private const int MAX_RANDOM_DEEP = 15;
        private const int FILE_FETCH_LIMIT = 1000;

        private static readonly string[] _drives = { "C", "D", "E", "F", "G", "Z" };
        private static readonly string[] _fileExtensions =
            {
                ".cs", ".js", ".py", ".java", ".cpp", ".rb", ".exe", ".bat",
                ".doc", ".docx", ".xls", ".xlsx"
            };

        private readonly string _fromFolder;
        private readonly bool _existing;
        private readonly IGenerator _wordGenerator = new WordRandomGenerator(1, 1);

        public FilePathGenerator(string fromFolder = null, bool existing = false)
        {
            _fromFolder = fromFolder;
            _existing = existing;

            if (_existing && !string.IsNullOrWhiteSpace(_fromFolder) && !Directory.Exists(_fromFolder))
            {
                throw new IOException($"Folder {_fromFolder} doesn't exist.");
            }

            if (!_fromFolder?.EndsWith(Path.DirectorySeparatorChar.ToString()) ?? false)
            {
                _fromFolder += Path.DirectorySeparatorChar;
            }
        }

        protected override string NextInternal() => _existing ? GetExistingPath() : GetRandomPath();

        private string GetRandomPath()
        {
            var result = new StringBuilder();

            var startingFolder = !string.IsNullOrWhiteSpace(_fromFolder) ? _fromFolder : GetRandomRoot();
            result.Append(startingFolder);
            var deep = NextRandomInteger(0, MAX_RANDOM_DEEP);
            for (var i = 0; i < deep; i++)
            {
                result.Append(_wordGenerator.Next()).Append(Path.DirectorySeparatorChar);
            }

            result.Append(_wordGenerator.Next());
            result.Append(NextRandomFromArray(_fileExtensions));
            return result.ToString();
        }

        private string GetRandomRoot()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT 
                ? $"{NextRandomFromArray(_drives)}:{Path.DirectorySeparatorChar}" 
                : "/";
        }

        private string GetExistingPath()
        {
            var startingFolder = !string.IsNullOrWhiteSpace(_fromFolder)
                                     ? _fromFolder
                                     : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var directory = new DirectoryInfo(startingFolder);
            var allFiles = GetFileNameEnumerator(directory).Take(FILE_FETCH_LIMIT).ToArray();
            if (allFiles.Length == 0)
            {
                throw new IOException($"Folder {startingFolder} doesn't contain any nested file.");
            }

            return NextRandomFromArray(allFiles);
        }

        private static IEnumerable<string> GetFileNameEnumerator(DirectoryInfo folder)
        {
            FileInfo[] files;
            try
            {
                files = folder.GetFiles();
            }
            catch (UnauthorizedAccessException)
            {
                files = Array.Empty<FileInfo>();
            }

            foreach (var file in files)
            {
                yield return file.FullName;
            }

            DirectoryInfo[] subFolders;
            try
            {
                subFolders = folder.GetDirectories();
            }
            catch (UnauthorizedAccessException)
            {
                subFolders = Array.Empty<DirectoryInfo>();
            }

            foreach (var subFolder in subFolders)
            {
                foreach (var filenames in GetFileNameEnumerator(subFolder))
                {
                    yield return filenames;
                }
            }
        } 
    }
}