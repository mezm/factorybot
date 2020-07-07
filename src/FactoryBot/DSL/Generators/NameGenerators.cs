using FactoryBot.DSL.Attributes;
using FactoryBot.Generators;
using FactoryBot.Generators.Strings;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Names generators
    /// </summary>
    public class NameGenerators
    {
#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random first name of a person
        /// </summary>
        /// <returns>First name</returns>
        [StringGeneratorFromResource(SourceNames.FIRST_NAMES)]
        public string FirstName() => default!;

        /// <summary>
        /// Generates random last name of a person
        /// </summary>
        /// <returns>Last name</returns>
        [StringGeneratorFromResource(SourceNames.LAST_NAMES)]
        public string LastName() => default!;

        /// <summary>
        /// Generates random full name of a person
        /// </summary>
        /// <returns>Full name</returns>
        [Generator(typeof(FullNameGenerator))]
        public string FullName() => default!;

        /// <summary>
        /// Generates random full name of a person
        /// </summary>
        /// <param name="format">Format of generation</param>
        /// <returns>Full name</returns>
        [Generator(typeof(FullNameGenerator))]
        public string FullName(FullNameFormat format) => default!;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}