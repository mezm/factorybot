using System;
using System.IO;

namespace FactoryBot.Generators.Strings
{
    public class RandomLineFromFileGenerator : RandomLineFromStreamGenerator
    {
        private readonly string _filename;

        public RandomLineFromFileGenerator(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("String should not be null or empty.", nameof(filename));
            }

            if (!File.Exists(filename))
            {
                throw new IOException($"File '{filename}' doesn't exist.");
            }

            _filename = filename;
        }

        protected override Stream OpenStream() => new FileStream(_filename, FileMode.Open);
    }
}