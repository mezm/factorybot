using System;

using FactoryBot.Generators.Strings;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class UrlGeneratorTest
    {
        [Test]
        public void GenerateAnyUrl()
        {
            var generator = new UrlGenerator(UriKind.RelativeOrAbsolute);

            var url1 = (string)generator.Next();
            var url2 = (string)generator.Next();

            Assert.That(url1, Is.Not.Null);
            Assert.That(url2, Is.Not.Null.And.Not.EqualTo(url1));
            Assert.That(Uri.IsWellFormedUriString(url1, UriKind.RelativeOrAbsolute));
            Assert.That(Uri.IsWellFormedUriString(url2, UriKind.RelativeOrAbsolute));
        }

        [Test]
        public void GenerateAnyAbsoluteUrl()
        {
            var generator = new UrlGenerator();
            var url = (string)generator.Next();
            Assert.That(Uri.IsWellFormedUriString(url, UriKind.Absolute));
        }

        [Test]
        public void GenerateAnyRelativeUrl()
        {
            var generator = new UrlGenerator(UriKind.Relative);
            var url = (string)generator.Next();
            Assert.That(Uri.IsWellFormedUriString(url, UriKind.Relative));
        }

        [Test]
        public void GenerateUrlWithoutPath()
        {
            var generator = new UrlGenerator(minPathSegments: 0, maxPathSegments: 0);

            var url = (string)generator.Next();

            Assert.That(Uri.IsWellFormedUriString(url, UriKind.Absolute));
            Assert.That(new Uri(url).PathAndQuery, Is.EqualTo("/"));
        }

        [Test]
        public void GenerateUrlWithPath()
        {
            var generator = new UrlGenerator(minPathSegments: 5, maxPathSegments: 10);

            var url = (string)generator.Next();

            Assert.That(Uri.IsWellFormedUriString(url, UriKind.Absolute));
            var path = new Uri(url).PathAndQuery;
            Assert.That(path, Is.Not.Contain("?"));
            Assert.That(path.Split('/'), Has.Length.InRange(5, 10));
        }

        [Test]
        public void GenerateUrlWithQueryParameters()
        {
            var generator = new UrlGenerator(schema: Uri.UriSchemeHttp, minQueryParams: 5, maxQueryParams: 10);

            var url = (string)generator.Next();

            Assert.That(Uri.IsWellFormedUriString(url, UriKind.Absolute), "Wrong uri: {0}", url);
            var path = new Uri(url).PathAndQuery;
            var paramsStart = path.IndexOf("?", StringComparison.OrdinalIgnoreCase);
            Assert.That(paramsStart, Is.GreaterThanOrEqualTo(0));
            Assert.That(path.Substring(paramsStart + 1).Split('&'), Has.Length.InRange(5, 10));
        }

        [Test]
        public void GenerateForConstantSchema()
        {
            var generator = new UrlGenerator(schema: "my");

            var url1 = (string)generator.Next();
            var url2 = (string)generator.Next();

            Assert.That(Uri.IsWellFormedUriString(url1, UriKind.Absolute));
            Assert.That(new Uri(url1).Scheme, Is.EqualTo("my"));
            Assert.That(Uri.IsWellFormedUriString(url2, UriKind.Absolute));
            Assert.That(new Uri(url2).Scheme, Is.EqualTo("my"));
        }

        [Test]
        public void GenerateForConstantHost()
        {
            var generator = new UrlGenerator(host: "my.info");

            var url1 = (string)generator.Next();
            var url2 = (string)generator.Next();

            Assert.That(Uri.IsWellFormedUriString(url1, UriKind.Absolute));
            Assert.That(new Uri(url1).Host, Is.EqualTo("my.info"));
            Assert.That(Uri.IsWellFormedUriString(url2, UriKind.Absolute));
            Assert.That(new Uri(url2).Host, Is.EqualTo("my.info"));
        }

        [Test]
        public void CreateWithInvalidParameters()
        {
            Assert.That(
                () => new UrlGenerator(minPathSegments: 10, maxPathSegments: 7),
                Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(
                () => new UrlGenerator(minQueryParams: 10, maxQueryParams: 5),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}