using System.Linq;
using System.Text.RegularExpressions;

namespace FactoryBot.Extensions
{
    public static class StringExtensions
    {
        public static string[] Words(this string str)
        {
            return Regex.Matches(str, @"(\w[\w-]*\w)|\w", RegexOptions.Compiled).Cast<Match>().Select(x => x.Value).ToArray();
        }

        public static string RemoveLineBreaks(this string str) => Regex.Replace(str, @"\r\n?|\n", x => RemoveLineBreakMatchEvaluator(str, x)).Trim();

        private static string RemoveLineBreakMatchEvaluator(string str, Match match)
        {
            if (match.Index == 0)
            {
                return string.Empty;
            }
            var previousChar = str[match.Index - 1];
            return char.IsWhiteSpace(previousChar) || char.IsControl(previousChar) ? string.Empty : " ";
        }
    }
}