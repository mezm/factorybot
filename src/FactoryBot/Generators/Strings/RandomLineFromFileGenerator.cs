using System.IO;

namespace FactoryBot.Generators.Strings
{
    public class RandomLineFromFileGenerator : RandomLineFromStreamGenerator
    {
        private readonly string _filename;

        public RandomLineFromFileGenerator(string filename)
        {
            Check.NotNullOrWhiteSpace(filename, nameof(filename));

            if (!File.Exists(filename))
            {
                throw new IOException($"File '{filename}' doesn't exist.");
            }

            _filename = filename;
        }

        protected override Stream OpenStream() => new FileStream(_filename, FileMode.Open);
    }
}