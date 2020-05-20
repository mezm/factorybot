using System;
using System.Collections.Generic;

namespace FactoryBot.Generators.Strings
{
    public class PhoneNumberGenerator : TypedGenerator<string>
    {
        private const string DEFAULT_TEMPLATE = "###-###-####";
        private const char PLACEHOLDER = '#';

        private readonly char[] _template;
        private readonly int[] _placeholderIndexes;

        public PhoneNumberGenerator(string template = DEFAULT_TEMPLATE)
        {
            if (string.IsNullOrWhiteSpace(template))
            {
                throw new ArgumentException("String should not be null or empty.", nameof(template));
            }

            _template = template.ToCharArray();
            var placeholders = new List<int>();

            for (var i = 0; i < _template.Length; i++)
            {
                if (_template[i] == PLACEHOLDER)
                {
                    placeholders.Add(i);
                }
            }

            _placeholderIndexes = placeholders.ToArray();
        }

        protected override string NextInternal()
        {
            var result = new char[_template.Length];
            _template.CopyTo(result, 0);

            var numberQueue = CreateRandomNumberString();
            var queuePosition = 0;
            foreach (var placeholderIndex in _placeholderIndexes)
            {
                if (queuePosition >= numberQueue.Length)
                {
                    numberQueue = CreateRandomNumberString();
                    queuePosition = 0;
                }
                
                result[placeholderIndex] = numberQueue[queuePosition];
                queuePosition++;
            }

            return new string(result);
        }

        private string CreateRandomNumberString() => NextRandomInteger(0, int.MaxValue).ToString();
    }
}