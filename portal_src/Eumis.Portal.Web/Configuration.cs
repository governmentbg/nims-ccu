using Eumis.Common.Config;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Eumis.Portal.Web
{
    public static class Constants
    {
        public const string SessionKey = "session";
        public const string SaveDraftActionName = "SaveDraft";

        public const string CaptchaModelName = "Captcha";
        public const string ActivationCodeAbbreviation = "aca";

        public const string MAIN_FORM = "main_form";
        public const string PARTIAL_SAVE_PROJECT = "PARTIAL_SAVE_PROJECT";
        public const string PARTIAL_SAVE_BFP_CONTRACT = "PARTIAL_SAVE_BFP_CONTRACT";
        public const string PARTIAL_SAVE_PROCUREMENTS = "PARTIAL_SAVE_PROCUREMENTS";
        public const string PARTIAL_SAVE_COMMUNICATION = "PARTIAL_SAVE_COMMUNICATION";
        public const string PARTIAL_SAVE_FINANCE_REPORT = "PARTIAL_SAVE_FINANCE_REPORT";
        public const string PARTIAL_SAVE_TECHNICAL_REPORT = "PARTIAL_SAVE_TECHNICAL_REPORT";
        public const string PARTIAL_SAVE_PAYMENT_REQUEST = "PARTIAL_SAVE_PAYMENT_REQUEST";
        public const string PARTIAL_SAVE_SPENDING_PLAN = "PARTIAL_SAVE_SPENDING_PLAN";
        public const string PARTIAL_SAVE_OFFER = "PARTIAL_SAVE_OFFER";
        public const string DISPLAY_PROJECT = "DISPLAY_PROJECT";
        public const string DISPLAY_MESSAGE = "DISPLAY_MESSAGE";
        public const string SIGNATURE_SUBMIT = "SIGNATURE_SUBMIT";

        public const string EMAIL_REGEX = @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";

        public static int MICRO_PAGE_ITEMS_COUNT { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:MicroPageItemsCount").TryParse() ?? 25;

        public static int PAGE_ITEMS_COUNT { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:PageItemsCount").TryParse() ?? 5;

        public static int PAGE_OFFERS_COUNT { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:PageOffersCount").TryParse() ?? 20;

        public static string PRODUCT_VERSION { get; } = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(Startup).Assembly.Location).ProductVersion;

        public static PagedList.Mvc.PagedListRenderOptions PagedListRenderOptions => new PagedList.Mvc.PagedListRenderOptions
        {
            ContainerDivClasses = new List<string>() { "txt-align-right" },
            Display = PagedList.Mvc.PagedListDisplayMode.IfNeeded,
            DisplayPageCountAndCurrentLocation = true,
            PageCountAndCurrentLocationFormat = Eumis.Common.Resources.Global.PageCountAndCurrentLocationFormat,
            UlElementClasses = new List<string>() { "pagination", "pagination-sm" }
        };

        public static bool IsReportActive { get; } = true.ToString().Equals(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:IsReportActive"), StringComparison.InvariantCultureIgnoreCase);
        
        public static bool IsOffersActive { get; } = true.ToString().Equals(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:IsOffersActive"), StringComparison.InvariantCultureIgnoreCase);

        public static bool UseDeprecatedZipPassword { get; } = true.ToString().Equals(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:UseDeprecatedZipPassword"), StringComparison.InvariantCultureIgnoreCase);

        public static string GoogleTrackingId { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:GoogleTrackingId");

        public static string ReCaptchaSiteKey { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:RecaptchaSiteKey");

        public static string ReCaptchaServerKey { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:ReCaptchaServerKey");

        public static bool SkipRecaptchaValidation { get; } = true.ToString().Equals(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:SkipRecaptchaValidation"), StringComparison.InvariantCultureIgnoreCase);

        private static int? TryParse(this string input)
        {
            if (int.TryParse(input, out var i))
            {
                return i;
            }

            return null;
        }
    }
}