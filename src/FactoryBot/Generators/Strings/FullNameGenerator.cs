namespace FactoryBot.Generators.Strings
{
    public class FullNameGenerator : TypedGenerator<string>
    {
        private readonly FullNameFormat _format;
        private readonly IGenerator _firstNameGenerator = new RandomLineFromResourceGenerator(SourceNames.FIRST_NAMES);
        private readonly IGenerator _lastNameGenerator = new RandomLineFromResourceGenerator(SourceNames.LAST_NAMES);

        public FullNameGenerator(FullNameFormat format = FullNameFormat.FirstNameLastName) => _format = format;

        protected override string NextInternal()
        {
            return _format == FullNameFormat.FirstNameLastName
                ? $"{_firstNameGenerator.Next()} {_lastNameGenerator.Next()}"
                : $"{_lastNameGenerator.Next()} {_firstNameGenerator.Next()}";
        }
    }
}