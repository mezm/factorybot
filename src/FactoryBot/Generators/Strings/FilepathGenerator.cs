using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FactoryBot.Generators.Strings
{
    public class FilePathGenerator : TypedGenerator<string>
    {
        private const int MaxRandomDeep = 15;
        private const int FileFetchLimit = 1000;

        private static readonly string[] Drives = { "C", "D", "E", "F", "G", "Z" };
        private static readonly string[] FileExtensions =
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

            if (!_fromFolder?.EndsWith("\\") ?? false)
            {
                _fromFolder += "\\";
            }
        }
        
        protected override string NextInternal()
        {
            return _existing ? GetExistingPath() : GetRandomPath();
        }

        private string GetRandomPath()
        {
            var result = new StringBuilder();

            var startingFolder = !string.IsNullOrWhiteSpace(_fromFolder)
                                     ? _fromFolder
                                     : $"{NextRandomFromArray(Drives)}:\\";
            result.Append(startingFolder);
            var deep = NextRandomInteger(0, MaxRandomDeep);
            for (var i = 0; i < deep; i++)
            {
                result.AppendFormat("{0}\\", _wordGenerator.Next());
            }

            result.Append(_wordGenerator.Next());
            result.Append(NextRandomFromArray(FileExtensions));
            return result.ToString();
        }

        private string GetExistingPath()
        {
            var startingFolder = !string.IsNullOrWhiteSpace(_fromFolder)
                                     ? _fromFolder
                                     : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var directory = new DirectoryInfo(startingFolder);
            var allFiles = GetFileNameEnumerator(directory).Take(FileFetchLimit).ToArray();
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