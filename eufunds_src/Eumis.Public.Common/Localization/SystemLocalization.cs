using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Routing;

namespace Eumis.Public.Common.Localization
{
    public class SystemLocalization
    {
        private static List<LocalizationPage> languagePages = new List<LocalizationPage>()
        {
            // new LocalizationPage(MVC.Home.Name, MVC.Home.ActionNames.Index),
            // new LocalizationPage(MVC.Home.Name, MVC.Home.ActionNames.ViewServices),
            // new LocalizationPage(MVC.Home.Name, MVC.Home.ActionNames.UseInstructions),
            // new LocalizationPage(MVC.Home.Name, MVC.Home.ActionNames.AccessibilityPolicy),
            // new LocalizationPage(MVC.Information.Information.Name, MVC.Information.Information.ActionNames.Index),
            // new LocalizationPage(MVC.Applications.Upload.Name, MVC.Applications.Upload.ActionNames.Upload),
        };

        public static System.Globalization.CultureInfo Culture { get; set; }

        public static bool IsLangaugePage(string controllerName, string actionName)
        {
            if (controllerName == null || actionName == null)
            {
                return false;
            }

            foreach (var page in languagePages)
            {
                if (page.ControllerName.ToLower() == controllerName.ToLower() && page.ActionName.ToLower() == actionName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsLangaugePage(RouteData routeData)
        {
            if (routeData == null)
            {
                return false;
            }

            return true; // IsLangaugePage((string)routeData.Values["Controller"], (string)routeData.Values["Action"]);
        }

        public static LocalizationLanguage GetCurrentLanguage()
        {
            string currentCulture = SystemLocalization.GetCurrentCulture();

            switch (currentCulture)
            {
                case Configuration.LANG_BG:
                    return LocalizationLanguage.Bulgarian;
                case Configuration.LANG_EN:
                    return LocalizationLanguage.English;
                default:
                    return LocalizationLanguage.Other;
            }
        }

        public static string GetCurrentCulture()
        {
            var currentCulture = string.Empty;
            if (Thread.CurrentThread.CurrentUICulture != null)
            {
                currentCulture = Thread.CurrentThread.CurrentUICulture.Name.ToLower().Substring(0, 2);
            }

            return currentCulture;
        }
    }
}
