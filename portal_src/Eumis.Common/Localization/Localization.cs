using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Routing;

namespace Eumis.Common.Localization
{
    public enum LocalizationLanguage
    {
        Bulgarian,
        English,
        Other
    }

    public class LocalizationPage
    {
        public string ControllerName { get; private set; }
        public string ActionName { get; private set; }

        public LocalizationPage(string controllerName, string actionName)
        {
            ControllerName = controllerName;
            ActionName = actionName;
        }
    }

    public class SystemLocalization
    {
        public static System.Globalization.CultureInfo Culture { get; set; }
        private static List<LocalizationPage> languagePages = new List<LocalizationPage>() 
        { 
            //new LocalizationPage(MVC.Home.Name, MVC.Home.ActionNames.Index),
            //new LocalizationPage(MVC.Home.Name, MVC.Home.ActionNames.ViewServices),
            //new LocalizationPage(MVC.Home.Name, MVC.Home.ActionNames.UseInstructions),
            //new LocalizationPage(MVC.Home.Name, MVC.Home.ActionNames.AccessibilityPolicy),
            //new LocalizationPage(MVC.Information.Information.Name, MVC.Information.Information.ActionNames.Index),
            //new LocalizationPage(MVC.Applications.Upload.Name, MVC.Applications.Upload.ActionNames.Upload),
        };

        public static bool IsLangaugePage(string controllerName, string actionName)
        {
            if (controllerName == null || actionName == null)
                return false;

            foreach (var page in languagePages)
            {
                if (page.ControllerName.ToLower() == controllerName.ToLower() && page.ActionName.ToLower() == actionName.ToLower())
                    return true;
            }

            return false;
        }

        public static bool IsLangaugePage(RouteData routeData)
        {
            if (routeData == null)
                return false;

            return true; // IsLangaugePage((string)routeData.Values["Controller"], (string)routeData.Values["Action"]);
        }

        public static LocalizationLanguage GetCurrentLanguage()
        {
            var currentCulture = GetCurrentCulture();

            switch (currentCulture)
            {
                case "bg":
                    return LocalizationLanguage.Bulgarian;
                case "en":
                    return LocalizationLanguage.English;
                default:
                    return LocalizationLanguage.Other;
            }
        }

        public static string GetCurrentCulture()
        {
            var currentCulture = String.Empty;
            if (Thread.CurrentThread.CurrentUICulture != null)
            {
                currentCulture = Thread.CurrentThread.CurrentUICulture.Name.ToLower().Substring(0, 2);
            }
            return currentCulture;
        }

        public static string GetDefaultCulture()
        {
            return "bg";
        }

        public static string GetDisplayName(string name, string nameAlt)
        {
#if DEBUG
            if (GetCurrentLanguage() == LocalizationLanguage.English)
            {
                return string.IsNullOrEmpty(nameAlt) ? "XOXOXOXOXO" : nameAlt;
            }
#else
            if (GetCurrentLanguage() == LocalizationLanguage.English)
            {
                return string.IsNullOrEmpty(nameAlt) ? name ?? string.Empty : nameAlt;
            }
#endif
            return name ?? string.Empty;
        }

        public static System.Globalization.CultureInfo GetLanguageCulture(LocalizationLanguage language)
        {
            System.Globalization.CultureInfo ci;

            switch (language)
            {
                case LocalizationLanguage.Bulgarian:
                    ci = new System.Globalization.CultureInfo("bg");
                    break;
                case LocalizationLanguage.English:
                    ci = new System.Globalization.CultureInfo("en");
                    break;
                case LocalizationLanguage.Other:
                    ci = new System.Globalization.CultureInfo("bg");
                    break;
                default:
                    ci = new System.Globalization.CultureInfo("bg");
                    break;
            }

            return ci;
        }
    }
}