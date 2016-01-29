using System.IO;
using FactoryBot.Utils;

namespace FactoryBot.Generators.Strings
{
    public abstract class RandomLineFromFileGenerator : TypedGenerator<string>
    {
        private readonly string _resourceName;
        private readonly int _sourceLength;

        protected RandomLineFromFileGenerator(string resourceName)
        {
            _resourceName = resourceName;
            Check.NotNullOrWhiteSpace(resourceName, nameof(resourceName));

            _sourceLength = (int)ResourceHelper.GetStreamLength(resourceName);
        }

        protected override string NextInternal()
        {
            return ResourceHelper.Read(_resourceName, Read);
        }

        private string Read(Stream stream, StreamReader reader)
        {
            string result = null;
            while (result == null)
            {
                var from = NextRandomInteger(0, _sourceLength);
                stream.Seek(from, SeekOrigin.Begin);
                reader.ReadLine(); // skipping first line because it could be incomplete
                result = reader.ReadLine();
            }

            return result;
        }
    }
}