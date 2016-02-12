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
                return Read(reader);
            }
        }

        protected abstract Stream OpenStream();

        protected virtual string Read(StreamReader reader)
        {
            var streamLength = reader.BaseStream.Length;
            if (streamLength == 0)
            {
                throw new IOException("Source is empty.");
            }

            string result = null;
            while (result == null)
            {
                var from = NextRandomInteger(0, (int)streamLength);
                reader.BaseStream.Seek(from, SeekOrigin.Begin);
                var line = reader.ReadLine(); 
                if (line == null)
                {
                    continue;
                }

                var lineLength = reader.CurrentEncoding.GetByteCount(line);
                var position = lineLength + from;
                if (position + GetLineBreakLength(reader, position) == streamLength) 
                {
                    // file contains a single line
                    reader.DiscardBufferedData();
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    return reader.ReadLine();
                }

                result = reader.ReadLine(); // skipping the first readed line because it could be incomplete
            }

            return result;
        }

        protected int GetLineBreakLength(StreamReader reader, long position)
        {
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(position, SeekOrigin.Begin);

            var ch = (char)reader.Read();
            switch (ch)
            {
                case '\n':
                    return 1;
                case '\r':
                    ch = (char)reader.Read();
                    return ch == '\n' ? 2 : 1;
                default:
                    return 0;
            }
        } 
    }
}