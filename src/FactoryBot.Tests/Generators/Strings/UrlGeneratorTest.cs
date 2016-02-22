using System;
using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class UrlGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void GenerateAnyUrl()
        {
            AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.Url(UriKind.RelativeOrAbsolute) },
                x => Assert.That(Uri.IsWellFormedUriString(x, UriKind.RelativeOrAbsolute)));
        }

        [Test]
        public void GenerateAnyAbsoluteUrl()
        {
            AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.Url(UriKind.Absolute) },
                x => Assert.That(Uri.IsWellFormedUriString(x, UriKind.Absolute)));
        }

        [Test]
        public void GenerateAnyRelativeUrl()
        {
            AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.Url(UriKind.Relative) },
                x => Assert.That(Uri.IsWellFormedUriString(x, UriKind.Relative)));
        }

        [Test]
        public void GenerateUrlWithoutPath()
        {
            AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Strings.Url(UriKind.Absolute, 0, 0, 0, 0, Uri.UriSchemeHttp, null) },
                x =>
                {
                    Assert.That(Uri.IsWellFormedUriString(x, UriKind.Absolute));
                    Assert.That(new Uri(x).PathAndQuery, Is.EqualTo("/"));
                });
        }

        [Test]
        public void GenerateUrlWithPath()
        {
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Strings.Url(UriKind.Absolute, 5, 10, 0, 0, Uri.UriSchemeHttp, null) },
                x =>
                {
                    Assert.That(Uri.IsWellFormedUriString(x, UriKind.Absolute));
                    var path = new Uri(x).PathAndQuery;
                    Assert.That(path, Is.Not.Contain("?"));
                    Assert.That(path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries), Has.Length.InRange(5, 10));
                });
        }

        [Test]
        public void GenerateUrlWithQueryParameters()
        {
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Strings.Url(UriKind.Absolute, 0, 0, 5, 10, Uri.UriSchemeHttp, null) },
                x =>
                {
                    Assert.That(Uri.IsWellFormedUriString(x, UriKind.Absolute), "Wrong uri: {0}", x);
                    var path = new Uri(x).PathAndQuery;
                    var paramsStart = path.IndexOf("?", StringComparison.OrdinalIgnoreCase);
                    Assert.That(paramsStart, Is.GreaterThanOrEqualTo(0));
                    Assert.That(path.Substring(paramsStart + 1).Split('&'), Has.Length.InRange(5, 10));
                });
        }

        [Test]
        public void GenerateForConstantSchema()
        {
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Strings.Url(UriKind.Absolute, 0, 0, 0, 0, "my", null) },
                x =>
                {
                    Assert.That(Uri.IsWellFormedUriString(x, UriKind.Absolute));
                    Assert.That(new Uri(x).Scheme, Is.EqualTo("my"));
                });
        }

        [Test]
        public void GenerateForConstantHost()
        {
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Strings.Url(UriKind.Absolute, 0, 0, 0, 0, null, "my.info") },
                x =>
                {
                    Assert.That(Uri.IsWellFormedUriString(x, UriKind.Absolute));
                    Assert.That(new Uri(x).Host, Is.EqualTo("my.info"));
                });
        }

        [Test]
        public void CreateWithInvalidParameters()
        {
            ExpectArgumentOutOfRangeInitException(
                x => new AllTypesModel { String = x.Strings.Url(UriKind.Absolute, 10, 7, 0, 0, null, null) });
            ExpectArgumentOutOfRangeInitException(
                x => new AllTypesModel { String = x.Strings.Url(UriKind.Absolute, 0, 0, 10, 5, null, null) });
        }
    }
}