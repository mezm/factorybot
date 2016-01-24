using System;
using System.IO;

using FactoryBot.Utils;

namespace FactoryBot.Generators.Strings
{
    public class StringRandomGenerator : TypedGenerator<string>
    {
        private const int CharSize = sizeof(char);

        private readonly int _sourceLength;
        private readonly int _minLength, _maxLength;
        private readonly Random _random = new Random();

        public StringRandomGenerator()
            : this(5, 100)
        {
        }

        public StringRandomGenerator(int minLength, int maxLength)
        {
            Check.GreaterThanZero(minLength, nameof(minLength));
            Check.GreaterThanZero(maxLength, nameof(maxLength));
            Check.MinMax(minLength, maxLength, nameof(minLength));

            _minLength = minLength;
            _maxLength = maxLength;
            _sourceLength = (int)ResourceHelper.GetStreamLength(SourceNames.RandomText);
        }
        
        protected override string NextInternal()
        {
            return ResourceHelper.Read(
                SourceNames.RandomText,
                (stream, reader) =>
                {
                    var from = _random.Next(0, _sourceLength / CharSize - 1) * CharSize;
                    var size = _random.Next(_minLength, _maxLength);
                    var buffer = new char[size];

                    stream.Seek(from, SeekOrigin.Begin);
                    reader.Read(buffer, 0, size);
                    return new string(buffer);
                });
        }
    }
}