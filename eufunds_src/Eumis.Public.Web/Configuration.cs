using System.Configuration;
using Eumis.Public.Common.Config;

namespace Eumis.Public.Web
{
    public static class Configuration
    {
        public static int MaxItemsPerPage { get; } = int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Public:MaxItemsPerPage"));

        public static int MaxNomItems { get; } = int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Public:MaxNomItems"));

        public static string GoogleTrackingId { get; } = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Public.Web:GoogleTrackingId");

        public static string ProductVersion { get; } = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(MvcApplication).Assembly.Location).ProductVersion;
    }
}
