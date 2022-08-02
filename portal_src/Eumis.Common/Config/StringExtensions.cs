using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common.Config
{
    public static class StringExtensions
    {
        public static string ExpandEnv(this string s)
        {
            return s == null ? null : Environment.ExpandEnvironmentVariables(s);
        }
    }
}
