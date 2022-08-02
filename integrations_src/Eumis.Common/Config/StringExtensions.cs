using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Eumis.Common.Config
{
    public static class StringExtensions
    {
        public static string ExpandEnv(this string s)
        {
            if (s == null)
            {
                return null;
            }
            else
            {
                string result = Environment.ExpandEnvironmentVariables(s);

                if (Regex.IsMatch(result, @"%(\w+)%"))
                {
                    throw new Exception($"Error on expand of environment variable {s}");
                }

                return result;
            }
        }

        public static string RemoveWhiteSpaces(this string value)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(value))
            {
                // Trim each line
                result = string.Join(
                    Environment.NewLine,
                    value.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                         .Select(l => l.Trim()));

                // Remove new lines
                result = result.Replace(Environment.NewLine, string.Empty).Trim();
            }

            return result;
        }
    }
}
