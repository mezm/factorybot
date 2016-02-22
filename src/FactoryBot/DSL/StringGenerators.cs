using System;
using System.Collections.Generic;

using FactoryBot.Generators.Collections;
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
        public string Words(int minWords, int maxWords) => default(string);

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

        [Generator(typeof(FilePathGenerator))]
        public string Filename() => default(string);

        [Generator(typeof(FilePathGenerator))]
        public string Filename(string fromFolder, bool existing) => default(string);

        [Generator(typeof(RandomFromListGenerator<string>))]
        public string RandomFromList(IReadOnlyList<string> source) => default(string);

        [Generator(typeof(SequenceFromListGenerator<string>))]
        public string SequenceFromList(IReadOnlyList<string> source) => default(string);

        [Generator(typeof(RandomLineFromFileGenerator))]
        public string RandomFromFile(string filename) => default(string);

        [Generator(typeof(SequenceStringFromFileGenerator))]
        public string SequenceFromFile(string filename) => default(string);

        [Generator(typeof(PhoneNumberGenerator))]
        public string PhoneNumber() => default(string);

        [Generator(typeof(PhoneNumberGenerator))]
        public string PhoneNumber(string template) => default(string);
    }
}