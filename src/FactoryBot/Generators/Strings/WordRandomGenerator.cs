using System;
using System.IO;
using System.Linq;
using System.Text;

using FactoryBot.Extensions;
using FactoryBot.Utils;

namespace FactoryBot.Generators.Strings
{
    public class WordRandomGenerator : TypedGenerator<string>
    {
        private const double AVARAGE_WORD_SIZE = 5.1;
        private const int MIN_BUFFER_SIZE = 30;

        private readonly int _approximateWordCountInSource, _minWords, _maxWords;
        
        public WordRandomGenerator(int minWords = 2, int maxWords = 7)
        {
            if (minWords <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minWords), minWords, "Should be greater than zero.");
            }
            if (maxWords <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxWords), maxWords, "Should be greater than zero.");
            }
            if (minWords > maxWords)
            {
                throw new ArgumentOutOfRangeException("Minimum should not be greater than maximum.");
            }

            _minWords = minWords;
            _maxWords = maxWords;
            _approximateWordCountInSource = (int)(ResourceHelper.GetStreamLength(SourceNames.RANDOM_TEXT)/AVARAGE_WORD_SIZE);
        }

        protected override string NextInternal() => ResourceHelper.Read(SourceNames.RANDOM_TEXT, Read);

        private string Read(StreamReader reader)
        {
            var targetWordCount = NextRandomInteger(_minWords, _maxWords);
            var bufferSize = (int)(targetWordCount * AVARAGE_WORD_SIZE);
            if (bufferSize < MIN_BUFFER_SIZE)
            {
                bufferSize = MIN_BUFFER_SIZE;
            }

            var buffer = new char[bufferSize];
            var from = (int)(NextRandomInteger(0, _approximateWordCountInSource - 1) * AVARAGE_WORD_SIZE);
            reader.BaseStream.Seek(from, SeekOrigin.Begin);

            var collectedWords = 0;
            var result = new StringBuilder(bufferSize);
            while (collectedWords < targetWordCount)
            {
                reader.Read(buffer, 0, buffer.Length);
                var words = new string(buffer).Words();
                if (words.Length == 0)
                {
                    continue;
                }

                var availableWords = words.Length - 2;
                if (availableWords < 1)
                {
                    continue;
                }

                var missingWords = targetWordCount - collectedWords;
                var wordsToTake = missingWords > availableWords ? availableWords : missingWords;
                foreach (var word in words.Skip(1).Take(wordsToTake))
                {
                    result.Append($"{word} ");
                }
                
                collectedWords += wordsToTake;
            }

            return result.ToString().TrimEnd();
        }

        public static WordRandomGenerator CreateSingleWordGenerator() => new WordRandomGenerator(1, 1);
    }
}