using FactoryBot.DSL.Attributes;
using FactoryBot.Generators;
using FactoryBot.Generators.Strings;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Address generators
    /// </summary>
    public class AddressGenerators
    {
#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random country 
        /// </summary>
        /// <returns>Country name</returns>
        [StringGeneratorFromResource(SourceNames.COUNTRIES)]
        public string Country() => default!;

        /// <summary>
        /// Generates random city 
        /// </summary>
        /// <returns>City name</returns>
        [StringGeneratorFromResource(SourceNames.CITIES)]
        public string City() => default!;

        /// <summary>
        /// Generates random USA state 
        /// </summary>
        /// <returns>State name</returns>
        [StringGeneratorFromResource(SourceNames.STATES)]
        public string State() => default!;

        /// <summary>
        /// Generates random postal code
        /// </summary>
        /// <param name="format">Postal code format</param>
        /// <returns>Postal code</returns>
        [Generator(typeof(PostalCodeGenerator))]
        public string PostalCode(PostalCodeFormat format) => default!;

        /// <summary>
        /// Generates random address
        /// </summary>
        /// <returns>Address</returns>
        [Generator(typeof(StreetAddressGenerator))]
        public string StreetAndBuilding() => default!; // todo should support formats as well

        /// <summary>
        /// Generates random phone number
        /// </summary>
        /// <returns>Phone number</returns>
        [Generator(typeof(PhoneNumberGenerator))]
        public string PhoneNumber() => default!;

        /// <summary>
        /// Generates random phone number by given template
        /// </summary>
        /// <param name="template">Phone number template. Use symbol # as a placeholder for a digit</param>
        /// <returns>Phone number</returns>
        [Generator(typeof(PhoneNumberGenerator))]
        public string PhoneNumber(string template) => default!;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}