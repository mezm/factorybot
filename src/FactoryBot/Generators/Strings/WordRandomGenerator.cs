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
        private const double AvarageWordSize = 5.1;

        private readonly Random _random = new Random();
        private readonly int _approximateWordCountInSource, _minWords, _maxWords;

        public WordRandomGenerator()
            : this(2, 7)
        {
        }

        public WordRandomGenerator(int minWords, int maxWords)
        {
            Check.GreaterThanZero(minWords, nameof(minWords));
            Check.GreaterThanZero(maxWords, nameof(maxWords));
            Check.MinMax(minWords, maxWords, nameof(minWords));

            _minWords = minWords;
            _maxWords = maxWords;
            _approximateWordCountInSource = (int)(ResourceHelper.GetStreamLength(SourceNames.RandomText)/AvarageWordSize);
        }

        protected override string NextInternal()
        {
            return ResourceHelper.Read(SourceNames.RandomText, Read);
        }

        private string Read(Stream stream, StreamReader reader)
        {
            var targetWordCount = _random.Next(_minWords, _maxWords);
            var buffer = new char[(int)(targetWordCount*AvarageWordSize)];
            var from = (int)(_random.Next(0, _approximateWordCountInSource - 1) * AvarageWordSize);
            stream.Seek(from, SeekOrigin.Begin);
            var collectedWords = 0;
            var result = new StringBuilder(targetWordCount);
            while (collectedWords < targetWordCount)
            {
                reader.Read(buffer, 0, buffer.Length);
                var words = new string(buffer).Words();
                if (words.Length == 0)
                {
                    continue;
                }

                var availableWords = words.Length - 2;
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
    }
}