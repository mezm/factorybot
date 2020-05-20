using System;
using System.IO;
using FactoryBot.Extensions;
using FactoryBot.Utils;

namespace FactoryBot.Generators.Strings
{
    public class StringRandomGenerator : TypedGenerator<string>
    {
        private const int CHAR_SIZE = sizeof(char);

        private readonly int _sourceLength;
        private readonly int _minLength, _maxLength;
        
        public StringRandomGenerator(int minLength = 5, int maxLength = 100)
        {
            if (minLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minLength), minLength, "Should be greater than zero.");
            }
            if (maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength), maxLength, "Should be greater than zero.");
            }
            if (minLength > maxLength)
            {
                throw new ArgumentOutOfRangeException("Minimum should not be greater than maximum.");
            }

            _minLength = minLength;
            _maxLength = maxLength;
            _sourceLength = (int)ResourceHelper.GetStreamLength(SourceNames.RANDOM_TEXT);
        }
        
        protected override string NextInternal() => ResourceHelper.Read(SourceNames.RANDOM_TEXT, Read);

        private string Read(StreamReader reader)
        {
            var size = NextRandomInteger(_minLength, _maxLength);
            var result = "";
            var buffer = new char[size];
            while (result.Length < size)
            {
                var from = NextRandomInteger(0, _sourceLength / CHAR_SIZE - 1) * CHAR_SIZE;
                reader.BaseStream.Seek(from, SeekOrigin.Begin);
                var readed = reader.Read(buffer, 0, size);
                result = new string(buffer, 0, readed).RemoveLineBreaks();
            }
            
            return result;
        }
    }
}