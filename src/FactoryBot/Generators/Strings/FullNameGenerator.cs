namespace FactoryBot.Generators.Strings
{
    public class FullNameGenerator : TypedGenerator<string>
    {
        private readonly FullNameFormat _format;
        private readonly IGenerator _firstNameGenerator = new FirstNameGenerator();
        private readonly IGenerator _lastNameGenerator = new LastNameGenerator();

        public FullNameGenerator()
            : this(FullNameFormat.FirstNameLastName)
        {
        }

        public FullNameGenerator(FullNameFormat format)
        {
            _format = format;
        }

        protected override string NextInternal()
        {
            return _format == FullNameFormat.FirstNameLastName
                ? $"{_firstNameGenerator.Next()} {_lastNameGenerator.Next()}"
                : $"{_lastNameGenerator.Next()} {_firstNameGenerator.Next()}";
        }
    }
}