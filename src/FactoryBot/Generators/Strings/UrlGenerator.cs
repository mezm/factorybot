using System;
using System.Text;

namespace FactoryBot.Generators.Strings
{
    public class UrlGenerator : TypedGenerator<string>
    {
        private static readonly string[] Schemas = { Uri.UriSchemeHttp, Uri.UriSchemeHttps };
        private static readonly string[] Domains = { ".com", ".net", ".org", ".edu", ".gov" };

        private readonly IGenerator _wordsGenerator = new WordRandomGenerator(1, 3);
        private readonly IGenerator _singleWordGenerator = new WordRandomGenerator(1, 1);
        private readonly UriKind _uriKind;
        private readonly int _minPathSegments, _maxPathSegments, _minQueryParams, _maxQueryParams;
        private readonly string _schema, _host;

        public UrlGenerator(
            UriKind uriKind = UriKind.Absolute,
            int minPathSegments = 0,
            int maxPathSegments = 5,
            int minQueryParams = 0,
            int maxQueryParams = 0,
            string schema = null,
            string host = null)
        {
            Check.MinMax(minPathSegments, maxPathSegments, nameof(minPathSegments));
            Check.MinMax(minQueryParams, maxQueryParams, nameof(minQueryParams));
            
            _uriKind = uriKind;
            _minPathSegments = minPathSegments;
            _maxPathSegments = maxPathSegments;
            _minQueryParams = minQueryParams;
            _maxQueryParams = maxQueryParams;
            _schema = schema;
            _host = host;
        }

        protected override string NextInternal()
        {
            var result = new StringBuilder();
            var generateAbsoluteUrl = _uriKind == UriKind.Absolute || (_uriKind == UriKind.RelativeOrAbsolute && NextRandomBool());

            if (generateAbsoluteUrl)
            {
                result.Append(GetSchema());
                result.Append(Uri.SchemeDelimiter); 
                result.Append(GetHost());
            }

            result.Append("/");

            var pathSegments = NextRandomInteger(_minPathSegments, _maxPathSegments);
            for (var i = 0; i < pathSegments; i++)
            {
                result.Append(GetNextRandomUrlPart());
                result.Append("/");
            }

            var queryParams = NextRandomInteger(_minQueryParams, _maxQueryParams);
            for (var i = 0; i < queryParams; i++)
            {
                result.Append(i > 0 ? "&" : "?");
                result.AppendFormat($"{GetNextQueryParamPart()}={GetNextQueryParamPart()}");
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GetHost()
        {
            if (!string.IsNullOrWhiteSpace(_host))
            {
                return _host;
            }

            var domain = Domains[NextRandomInteger(0, Domains.Length - 1)];
            return GetNextRandomUrlPart() + domain;
        }

        private string GetSchema()
        {
            return string.IsNullOrWhiteSpace(_schema) ? Schemas[NextRandomInteger(0, Schemas.Length - 1)] : _schema;
        }

        private string GetNextRandomUrlPart()
        {
            return ((string)_wordsGenerator.Next()).Replace(" ", "");
        }

        private string GetNextQueryParamPart()
        {
            return (string)_singleWordGenerator.Next();
        }
    }
}