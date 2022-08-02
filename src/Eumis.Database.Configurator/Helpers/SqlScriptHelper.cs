using System;
using System.Globalization;

namespace Eumis.Database.Configurator.Helpers
{
    internal static class SqlScriptHelper
    {
        public static string ToString(string value)
        {
            return !string.IsNullOrWhiteSpace(value) ? string.Format("N'{0}'", value.Replace("'", "''")) : "NULL";
        }

        public static string ToString(int value)
        {
            return value.ToString();
        }

        public static string ToString(int? value)
        {
            return value.HasValue ? ToString(value.Value) : "NULL";
        }

        public static string ToString(bool value)
        {
            return value ? "1" : "0";
        }

        public static string ToString(bool? value)
        {
            return value.HasValue ? ToString(value.Value) : "NULL";
        }

        public static string ToString(decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToString(decimal? value)
        {
            return value.HasValue ? ToString(value.Value) : "NULL";
        }

        public static string ToString(Guid value)
        {
            return string.Format("N'{0}'", value);
        }

        public static string ToString(Guid? value)
        {
            return value.HasValue ? ToString(value.Value) : "NULL";
        }

        public static string ToString(DateTime value)
        {
            return string.Format("'{0:yyyy-MM-dd HH:mm:ss}'", value);
        }

        public static string ToString(DateTime? value)
        {
            return value.HasValue ? ToString(value.Value) : "NULL";
        }
    }
}
