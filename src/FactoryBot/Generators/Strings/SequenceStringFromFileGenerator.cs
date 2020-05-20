using System.IO;

namespace FactoryBot.Generators.Strings
{
    public class SequenceStringFromFileGenerator : RandomLineFromFileGenerator
    {
        private long _position;

        public SequenceStringFromFileGenerator(string filename)
            : base(filename)
        {
        }

        protected override string Read(StreamReader reader)
        {
            string? line = null;
            while (line == null)
            {
                if (reader.BaseStream.Length == 0)
                {
                    throw new IOException("Source is empty.");
                }

                reader.BaseStream.Seek(_position, SeekOrigin.Begin);
                line = reader.ReadLine();
                if (line == null)
                {
                    _position = 0;
                    continue;
                }

                _position += reader.CurrentEncoding.GetByteCount(line);
                _position += GetLineBreakLength(reader, _position);
            }

            return line;
        }
    }
}