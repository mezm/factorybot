namespace FactoryBot.Generators.Strings
{
    public class FullNameGenerator : TypedGenerator<string>
    {
        private readonly FullNameFormat _format;
        private readonly IGenerator _firstNameGenerator = ResourceBasedGeneratorFactory.CreateFirstNameGenerator();
        private readonly IGenerator _lastNameGenerator = ResourceBasedGeneratorFactory.CreateLastNameGenerator();

        public FullNameGenerator(FullNameFormat format = FullNameFormat.FirstNameLastName) => _format = format;

        protected override string NextInternal()
        {
            return _format == FullNameFormat.FirstNameLastName
                ? $"{_firstNameGenerator.Next()} {_lastNameGenerator.Next()}"
                : $"{_lastNameGenerator.Next()} {_firstNameGenerator.Next()}";
        }
    }
}