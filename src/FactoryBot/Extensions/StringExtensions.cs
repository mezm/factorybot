using System.Linq;
using System.Text.RegularExpressions;

namespace FactoryBot.Extensions
{
    public static class StringExtensions
    {
        public static string[] Words(this string str)
        {
            Check.NotNull(str, nameof(str));

            return Regex.Matches(str, @"(\w[\w-]*\w)|\w", RegexOptions.Compiled).Cast<Match>().Select(x => x.Value).ToArray();
        }
    }
}