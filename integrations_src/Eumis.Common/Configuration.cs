using Eumis.Common.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common
{
    public static class Configuration
    {
        public static bool EnableRequestResponseLogging { get; } = bool.Parse(ConfigurationManager.AppSettings["Eumis.Common:EnableRequestResponseLogging"]);
    }
}
