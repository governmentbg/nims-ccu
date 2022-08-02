using Eumis.Common.Config;
using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;

namespace Eumis.Web.Host.Nancy.Models
{
    public class LayoutModel
    {
        private static string productVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(Startup).Assembly.Location).ProductVersion;

        public string ProductVersion { get; } = productVersion;

        public bool IsBgVersion { get; } = Thread.CurrentThread.CurrentCulture.Name == SystemLocalization.Bg_BG;

        public bool ShowLanguageSwitcher { get; } = "true".Equals(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:ShowLanguageSwitcher"), StringComparison.InvariantCultureIgnoreCase);

        public string GoogleTrackingId { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:GoogleTrackingId");
    }
}