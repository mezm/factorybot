using System.IO;

namespace FactoryBot.Generators.Strings
{
    public abstract class RandomLineFromStreamGenerator : TypedGenerator<string>
    {
        protected override string NextInternal()
        {
            using (var stream = OpenStream())
            using (var reader = new StreamReader(stream))
            {
                return Read(stream, reader);
            }
        }

        protected abstract Stream OpenStream();

        private string Read(Stream stream, StreamReader reader)
        {
            string result = null;
            while (result == null)
            {
                var from = NextRandomInteger(0, (int)stream.Length);
                stream.Seek(from, SeekOrigin.Begin);
                reader.ReadLine(); // skipping first line because it could be incomplete
                result = reader.ReadLine();
            }

            return result;
        }
    }
}