using Eumis.Common.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Communicators
{
    public static class RegixConfig
    {
        private static string serverLocation;
        private static int apiRequestTimeout = 0;

        public static string ServerLocation
        {
            get
            {
                if (serverLocation == null)
                {
                    string l = ConfigurationManager.AppSettings.GetWithEnv("Eumis.ApplicationServices:RegixAPI");
                    if (!l.EndsWith("/"))
                    {
                        l += "/";
                    }

                    serverLocation = l;
                }

                return serverLocation;
            }
        }

        public static int ApiRequestTimeout
        {
            get
            {
                if (apiRequestTimeout == 0)
                {
                    apiRequestTimeout = int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.ApplicationServices:ApiRequestTimeout"));
                }

                return apiRequestTimeout;
            }
        }
    }
}
