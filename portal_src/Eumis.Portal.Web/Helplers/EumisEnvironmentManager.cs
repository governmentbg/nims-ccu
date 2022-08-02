using Eumis.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace Eumis.Portal.Web.Helpers
{
    public enum EumisEnvironment
    {
        InnerDev = 1,
        InnerTest = 2,

        ProductionTest = 3,
        ProductionLearn = 4,

        Production = 5,

        ProductionServices = 6,

        Unknown = 100
    }

    public static partial class EumisEnvironmentManager
    {
        public static EumisEnvironment Current
        {
            get
            {
                var environmentKeyName = System.Configuration.ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:EnvironmentNameKey");

                switch (environmentKeyName)
                {
                    case "InnerDevEnvironmentName":
                        return EumisEnvironment.InnerDev;
                    case "DavidCIEnvironmentName":
                        return EumisEnvironment.InnerTest;
                    case "LearnEnvironmentName":
                        return EumisEnvironment.ProductionLearn;
                    case "TestEnvironmentName":
                        return EumisEnvironment.ProductionTest;
                    case "ServicesEnvironmentName":
                        return EumisEnvironment.ProductionServices;
                    case "ProdEnvironmentName":
                        return EumisEnvironment.Production;
                    default:
                        return EumisEnvironment.Unknown;
                }
            }
        }

        public static string CurrentText
        {
            get
            {
                return
                    Eumis.Common.Resources.Global.ResourceManager.GetString(
                        System.Configuration.ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:EnvironmentNameKey"));
            }
        }
    }
}