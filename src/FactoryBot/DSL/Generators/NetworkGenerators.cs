using FactoryBot.DSL.Attributes;
using FactoryBot.Generators;
using FactoryBot.Generators.Strings;
using System;

namespace FactoryBot.DSL.Generators
{
    public class NetworkGenerators 
    {
#pragma warning disable IDE0060 // Remove unused parameter

        [Generator(typeof(EmailGenerator))]
        public string Email() => default;

        [Generator(typeof(UrlGenerator))]
        public string Url() => default;

        [Generator(typeof(UrlGenerator))]
        public string Url(UriKind uriKind) => default;

        [Generator(typeof(UrlGenerator))]
        public string Url(string schema, string host) => default;

        [Generator(typeof(UrlGenerator))]
        public string Url(
            UriKind uriKind,
            int minPathSegments,
            int maxPathSegments,
            int minQueryParams,
            int maxQueryParams,
            string schema,
            string host) => default;

        [Generator(typeof(HostnameGenerator))]
        public string Hostname() => default;

        [Generator(typeof(HostnameGenerator))]
        public string Hostname(int minSubdomains, int maxSubdomains) => default;

        [StringGeneratorFromResource(SourceNames.TOP_LEVEL_DOMAINS)]
        public string TopLevelDomain() => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}