using System;
using System.Collections.Generic;
using FactoryBot.DSL.Attributes;
using FactoryBot.Generators;
using FactoryBot.Generators.Collections;
using FactoryBot.Generators.Strings;

namespace FactoryBot.DSL.Generators
{
    public class StringGenerators
    {
        public AddressGenerators Address { get; } = new AddressGenerators();

        [Generator(typeof(StringRandomGenerator))]
        public string Any() => default;

#pragma warning disable IDE0060 // Remove unused parameter

        [Generator(typeof(StringRandomGenerator))]
        public string Any(int minLength, int maxLength) => default;

        [Generator(typeof(WordRandomGenerator))]
        public string Words() => default;

        [Generator(typeof(WordRandomGenerator))]
        public string Words(int minWords, int maxWords) => default;

        [StringGeneratorFromResource(SourceNames.FIRST_NAMES)]
        public string FirstName() => default;

        [StringGeneratorFromResource(SourceNames.LAST_NAMES)]
        public string LastName() => default;

        [Generator(typeof(FullNameGenerator))]
        public string FullName() => default;

        [Generator(typeof(FullNameGenerator))]
        public string FullName(FullNameFormat format) => default;

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

        [Generator(typeof(FilePathGenerator))]
        public string Filename() => default;

        [Generator(typeof(FilePathGenerator))]
        public string Filename(string fromFolder, bool existing) => default;

        [Generator(typeof(RandomFromListGenerator<string>))]
        public string RandomFromList(IReadOnlyList<string> source) => default;

        [Generator(typeof(RandomFromListGenerator<string>))]
        public string RandomFromList(params string[] source) => default;

        [Generator(typeof(SequenceFromListGenerator<string>))]
        public string SequenceFromList(IReadOnlyList<string> source) => default;

        [Generator(typeof(SequenceFromListGenerator<string>))]
        public string SequenceFromList(params string[] source) => default;

        [Generator(typeof(RandomLineFromFileGenerator))]
        public string RandomFromFile(string filename) => default;

        [Generator(typeof(SequenceStringFromFileGenerator))]
        public string SequenceFromFile(string filename) => default;

        [Generator(typeof(PhoneNumberGenerator))]
        public string PhoneNumber() => default;

        [Generator(typeof(PhoneNumberGenerator))]
        public string PhoneNumber(string template) => default;

        [Generator(typeof(GuidGenerator))]
        public string Guid() => default;

#pragma warning restore IDE0060 // Remove unused parameter
    }
}