using System;

namespace FactoryBot.Generators.Strings
{
    public class PostalCodeGenerator : TypedGenerator<string>
    {
        private readonly PostalCodeFormat _format;

        public PostalCodeGenerator(PostalCodeFormat format) => _format = format;

        protected override string NextInternal()
        {
            if (_format == PostalCodeFormat.Zip)
            {
                return NextRandomInteger(10000, 99999).ToString();
            }

            throw new NotSupportedException();
        }
    }
}
