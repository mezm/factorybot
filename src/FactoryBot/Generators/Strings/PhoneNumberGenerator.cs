using System.Collections.Generic;

namespace FactoryBot.Generators.Strings
{
    public class PhoneNumberGenerator : TypedGenerator<string>
    {
        private const string DefaultTemplate = "###-###-####";
        private const char Placeholder = '#';

        private readonly char[] _template;
        private readonly int[] _placeholderIndexes;

        public PhoneNumberGenerator(string template = DefaultTemplate)
        {
            Check.NotNullOrWhiteSpace(template, nameof(template));

            _template = template.ToCharArray();
            var placeholders = new List<int>();

            for (var i = 0; i < _template.Length; i++)
            {
                if (_template[i] == Placeholder)
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

        private string CreateRandomNumberString()
        {
            return NextRandomInteger(0, int.MaxValue).ToString();
        }
    }
}