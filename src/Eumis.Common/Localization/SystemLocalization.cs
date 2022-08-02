namespace Eumis.Common.Localization
{
    public static class SystemLocalization
    {
        public const string Bg_BG = "bg-BG";

        public const string En_GB = "en-GB";

        public const string DefaultCulture = Bg_BG;

        public static string GetDisplayName(string name, string nameAlt)
        {
#if DEBUG
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == En_GB)
            {
                return string.IsNullOrEmpty(nameAlt) ? "XOXOXOXOXO" : nameAlt;
            }
#else
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == En_GB)
            {
                return string.IsNullOrEmpty(nameAlt) ? name ?? string.Empty : nameAlt;
            }
#endif
            return name ?? string.Empty;
        }

        public static string GetPortalLanguageRoute()
        {
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == En_GB)
            {
                return "en";
            }

            return "bg";
        }
    }
}
