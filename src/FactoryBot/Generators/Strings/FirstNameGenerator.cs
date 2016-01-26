using System.IO;

using FactoryBot.Utils;

namespace FactoryBot.Generators.Strings
{
    public class FirstNameGenerator : TypedGenerator<string>
    {
        private readonly int _sourceLength = (int)ResourceHelper.GetStreamLength(SourceNames.FirstNames);
        
        protected override string NextInternal()
        {
            return ResourceHelper.Read(SourceNames.FirstNames, Read);
        }

        private string Read(Stream stream, StreamReader reader)
        {
            string result = null;
            while (result == null)
            {
                var from = NextRandom(0, _sourceLength);
                stream.Seek(from, SeekOrigin.Begin);
                reader.ReadLine(); // skipping first line because it could be incomplete
                result = reader.ReadLine();
            }

            return result;
        }
    }
}