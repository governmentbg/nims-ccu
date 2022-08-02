using Eumis.Common.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host.Helpers
{
    public static class Configuration
    {
        public static string IsunUser { get; } = ConfigurationManager.AppSettings.GetWithEnv("Monitorstat.IntegrationEumis.Host:Username");

        public static string IsunPassword { get; } = ConfigurationManager.AppSettings.GetWithEnv("Monitorstat.IntegrationEumis.Host:Password");

        public static string ServerLocation { get; } = ConfigurationManager.AppSettings.GetWithEnv("Monitorstat.IntegrationEumis.Host:IntegrationServerLocation");

        public static string DefaultFilename { get; } = ConfigurationManager.AppSettings.GetWithEnv("Monitorstat.IntegrationEumis.Host:DefaultFilename");

        public static int ApiRequestTimeout { get; } = Convert.ToInt32(ConfigurationManager.AppSettings.GetWithEnv("Monitorstat.IntegrationEumis.Host:ApiRequestTimeout"));

        public static string InstallationName { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:InstallationName");

        public static string AuthKey { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:Key");

        public static string AuthPreamble { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:Preamble");

        public static TimeSpan AuthExpirationTimeSpan { get; } = TimeSpan.FromMinutes(double.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:TokenExpirationMinutes")));

        public static string InternalBlobServerLocation { get;  } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:InternalBlobServerLocation");
    }
}
