using Eumis.Public.Common.Config;
using System;
using System.Configuration;

namespace Eumis.Public.Common
{
    public static class Configuration
    {
        public const int OP_DEFAULT_ID = 0;
        public const int PR_DEFAULT_ID = 0;

        public const int PR_BULGARIA_ID = 888;
        public const int PR_INTERNATIONAL_ID = 999;

        public const int BULGARIA_COUNTRY_ID = 23;
        public const string BULGARIA_COUNTRY_CODE = "BG";

        public const string LANG_BG = "bg";
        public const string LANG_EN = "en";

        public const string HTML_ENQUERIES_ID = "enquiries";
        public const string HTML_OPERATIONAL_PROGRAMS_ID = "operational_programs";

        public const string CALENDAR_FORMAT_MONTH = "yyyyMM";
        public const string CALENDAR_FORMAT_DATE = "yyyyMMdd";
        public const string CALENDAR_FORMAT_DAY = "yyyy-MM-dd";

        public static string PortalLocation { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Public.Common:PortalLocation");

        public static DateTime AbsoluteExpiration { get; } = DateTime.Now.AddSeconds(int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Public.Common:CacheExpirationInSeconds")));
    }
}
