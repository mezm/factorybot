using FactoryBot.Generators.Strings;

namespace FactoryBot.Generators
{
    internal static class ResourceBasedGeneratorFactory
    {
        public static IGenerator CreateFirstNameGenerator() => new RandomLineFromResourceGenerator(SourceNames.FIRST_NAMES);

        public static IGenerator CreateLastNameGenerator() => new RandomLineFromResourceGenerator(SourceNames.LAST_NAMES);

        public static IGenerator CreateTopLevelDomainGenerator() => new RandomLineFromResourceGenerator(SourceNames.TOP_LEVEL_DOMAINS);
    }
}