using System;

namespace Eumis.Public.Common.Config
{
    public static class StringExtensions
    {
        public static string ExpandEnv(this string s)
        {
            return s == null ? null : Environment.ExpandEnvironmentVariables(s);
        }
    }
}
