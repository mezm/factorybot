using System;

using FactoryBot.Generators.Strings;

namespace FactoryBot.DSL
{
    public class StringGenerators
    {
        [Generator(typeof(StringRandomGenerator))]
        public string Any() => default(string);

        [Generator(typeof(StringRandomGenerator))]
        public string Any(int minLength, int maxLength) => default(string);

        [Generator(typeof(WordRandomGenerator))]
        public string Words() => default(string);

        [Generator(typeof(WordRandomGenerator))]
        public string Words(int minCount, int maxCount) => default(string);

        [Generator(typeof(FirstNameGenerator))]
        public string FirstName() => default(string);

        [Generator(typeof(LastNameGenerator))]
        public string LastName() => default(string);

        [Generator(typeof(FullNameGenerator))]
        public string FullName() => default(string);

        [Generator(typeof(FullNameGenerator))]
        public string FullName(FullNameFormat format) => default(string);

        [Generator(typeof(UrlGenerator))]
        public string Url() => default(string);

        [Generator(typeof(UrlGenerator))]
        public string Url(UriKind uriKind) => default(string);

        [Generator(typeof(UrlGenerator))]
        public string Url(string schema, string host) => default(string);

        [Generator(typeof(UrlGenerator))]
        public string Url(
            UriKind uriKind,
            int minPathSegments,
            int maxPathSegments,
            int minQueryParams,
            int maxQueryParams,
            string schema,
            string host) => default(string);
    }
}