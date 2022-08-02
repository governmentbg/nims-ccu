using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Eumis.Common
{
    public static class StringExtensions
    {
        private static Regex paragraphRegex = new Regex(@"(\n|^)(.+)(?=\n|$)", RegexOptions.Compiled);
        private static Regex urlRegex = new Regex(@"(https?|ftp)?:\/{2}[^\s/$.?#].[^\s]*", RegexOptions.Compiled);

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string TruncateWithEllipsis(this string value, int maxLength)
        {
            if (maxLength <= 3)
            {
                throw new ArgumentException("Max length must be greater than 3");
            }

            if (value == null ||
                value.Length <= maxLength)
            {
                return value;
            }

            return Truncate(value, maxLength - 3) + "...";
        }

        public static string MakeHtml(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            StringBuilder res = new StringBuilder();

            var paragraphMatches = paragraphRegex.Matches(value);
            foreach (Match paragraphMatch in paragraphMatches)
            {
                var paragraph = paragraphMatch.Groups[2].Value;

                if (!string.IsNullOrWhiteSpace(paragraph))
                {
                    var encodedParagraph = WebUtility.HtmlEncode(paragraph);

                    encodedParagraph = urlRegex.Replace(encodedParagraph, "<a href=\"$&\" target=\"_blank\">$&</a>");

                    res.AppendFormat("<p>{0}</p>\n", encodedParagraph);
                }
            }

            return res.ToString();
        }

        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            if (!char.IsUpper(s[0]))
            {
                return s;
            }

            char[] chars = s.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                bool hasNext = i + 1 < chars.Length;
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }

                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }

            return new string(chars);
        }

        public static bool ContainsNonASCIICharacter(this string s) => s.ToCharArray().Any(CharExtensions.NonASCIICharacterPredicate);
    }
}
