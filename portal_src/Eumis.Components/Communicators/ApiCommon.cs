using Eumis.Common.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Components.Communicators
{
    class ApiCommon
    {
        private static string serverLocation;
        public static string ServerLocation
        {
            get
            {
                if (serverLocation == null)
                {
                    string l = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Components:ServerLocation");
                    if (!l.EndsWith("/"))
                    {
                        l += "/";
                    }

                    serverLocation = l;
                }

                return serverLocation;
            }
        }

        private static int apiRequestTimeout = 0;
        public static int ApiRequestTimeout
        {
            get
            {
                if (apiRequestTimeout == 0)
                {
                    apiRequestTimeout = Int32.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Components:ApiRequestTimeout"));
                }

                return apiRequestTimeout;
            }
        }
    }
}
