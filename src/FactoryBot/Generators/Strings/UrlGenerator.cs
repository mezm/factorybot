using System;
using System.Text;

namespace FactoryBot.Generators.Strings
{
    public class UrlGenerator : TypedGenerator<string>
    {
        private readonly string[] _schemas = { Uri.UriSchemeFtp, Uri.UriSchemeHttp, Uri.UriSchemeHttps, Uri.UriSchemeFile };
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

            if (minQueryParams > 0 && string.IsNullOrWhiteSpace(schema))
            {
                throw new ArgumentException("Query parameters are supported only with schema.", nameof(minQueryParams));
            }

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
                var schema = string.IsNullOrWhiteSpace(_schema) ? _schemas[NextRandomInteger(0, _schemas.Length - 1)] : _schema;
                var host = string.IsNullOrWhiteSpace(_host) ? $"{GetNextRandomUrlPart()}.com" : _host; // TODO: get random
                result.Append(schema);
                result.Append(Uri.SchemeDelimiter); 
                result.Append(host);  
            } 

            var pathSegments = NextRandomInteger(_minPathSegments, _maxPathSegments);
            for (var i = 0; i < pathSegments; i++)
            {
                result.Append("/");
                result.Append(GetNextRandomUrlPart());
            }

            var queryParams = NextRandomInteger(_minQueryParams, _maxQueryParams);
            for (var i = 0; i < queryParams; i++)
            {
                result.Append(i > 0 ? "&" : "?");
                result.AppendFormat($"{GetNextQueryParamPart()}={GetNextQueryParamPart()}");
            }

            return result.ToString();
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