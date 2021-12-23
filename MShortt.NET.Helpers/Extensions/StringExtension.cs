using MShortt.NET.Helpers.Constants;
using System.Text.RegularExpressions;

namespace MShortt.NET.Helpers.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        ///     Trims all leading and trailing white space characters from the string if it is not null or empty. If character arguments are provided,
        ///     leading and trailing occurrences of these chars will be trimmed instead.
        /// </summary>
        public static string TrimIfNotNullOrEmpty(this string value, params char[] trimChars)
        {
            return string.IsNullOrEmpty(value)
                ? value
                : value.Trim(trimChars);
        }

        /// <summary>Trims all leading and trailing white space characters from the string, and replaces instances of double spacing, triple spacing, etc. with a single space.</summary>
        public static string RemoveRedundantSpacing(this string value)
        {
            return value == string.Empty
                ? value
                : Regex.Replace(value.Trim(), @"\s{2,}", WhiteSpaceConstant.String);
        }
    }
}
