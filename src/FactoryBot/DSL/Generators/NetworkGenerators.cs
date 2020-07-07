using FactoryBot.DSL.Attributes;
using FactoryBot.Generators;
using FactoryBot.Generators.Strings;
using System;

namespace FactoryBot.DSL.Generators
{
    /// <summary>
    /// Network related generators
    /// </summary>
    public class NetworkGenerators 
    {
#pragma warning disable IDE0060 // Remove unused parameter
        /// <summary>
        /// Generates random email
        /// </summary>
        /// <returns>Email</returns>
        [Generator(typeof(EmailGenerator))]
        public string Email() => default!;

        /// <summary>
        /// Generates random URL
        /// </summary>
        /// <returns>URL</returns>
        [Generator(typeof(UrlGenerator))]
        public string Url() => default!;

        /// <summary>
        /// Generates random URL
        /// </summary>
        /// <param name="uriKind">URL kind</param>
        /// <returns>URL</returns>
        [Generator(typeof(UrlGenerator))]
        public string Url(UriKind uriKind) => default!;

        /// <summary>
        /// Generates random URL
        /// </summary>
        /// <param name="schema">URL schema</param>
        /// <param name="host">Host</param>
        /// <returns>URL</returns>
        [Generator(typeof(UrlGenerator))]
        public string Url(string schema, string host) => default!;

        /// <summary>
        /// Generates random URL
        /// </summary>
        /// <param name="uriKind">URL kind</param>
        /// <param name="minPathSegments">Minimum count of path segments in URL</param>
        /// <param name="maxPathSegments">Maximum count of path segments in URL</param>
        /// <param name="minQueryParams">Minimum count of query parameters in URL</param>
        /// <param name="maxQueryParams">Maximum count of query parameters in URL</param>
        /// <param name="schema">URL schema</param>
        /// <param name="host">Host</param>
        /// <returns>URL</returns>
        [Generator(typeof(UrlGenerator))]
        public string Url(
            UriKind uriKind,
            int minPathSegments,
            int maxPathSegments,
            int minQueryParams,
            int maxQueryParams,
            string schema,
            string host) => default!;

        /// <summary>
        /// Generates random hostname, e.g. "wiki.my-company.net"
        /// </summary>
        /// <returns>Hostname</returns>
        [Generator(typeof(HostnameGenerator))]
        public string Hostname() => default!;

        /// <summary>
        /// Generates random hostname, e.g. "wiki.my-company.net"
        /// </summary>
        /// <param name="minSubdomains">Minimum count of subdomains</param>
        /// <param name="maxSubdomains">Maximum count of subdomains</param>
        /// <returns>Hostname</returns>
        [Generator(typeof(HostnameGenerator))]
        public string Hostname(int minSubdomains, int maxSubdomains) => default!;

        /// <summary>
        /// Generates random top level domain, e.g. "com", "net", etc.
        /// </summary>
        /// <returns>Top level domain</returns>
        [StringGeneratorFromResource(SourceNames.TOP_LEVEL_DOMAINS)]
        public string TopLevelDomain() => default!;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}