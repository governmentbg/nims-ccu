using System.Collections.Generic;
using System.Collections.ObjectModel;
using Eumis.Public.Common.Config;
using Eumis.Public.Resources;

namespace Eumis.Public.Web.InfrastructureClasses.PublicEnvironment
{
    public static class EnvironmentManager
    {
        private const string EnvironmentNameAppKey = "Eumis.Public.Web:EnvironmentNameKey";

        private static readonly ReadOnlyDictionary<string, EnvironmentTuple> EnvDict = new ReadOnlyDictionary<string, EnvironmentTuple>(new Dictionary<string, EnvironmentTuple>()
        {
            { "InnerDevEnvironmentName", new EnvironmentTuple("InnerDevEnvironmentName", EnvironmentType.InnerDev, () => Texts.Global_EnvInnerDev) },
            { "InnerTestEnvironmentName", new EnvironmentTuple("InnerDevEnvironmentName", EnvironmentType.InnerTest, () => Texts.Global_EnvInnerTest) },
            { "LearnEnvironmentName", new EnvironmentTuple("InnerDevEnvironmentName", EnvironmentType.ProductionLearn, () => Texts.Global_EnvProductionLearn) },
            { "TestEnvironmentName", new EnvironmentTuple("InnerDevEnvironmentName", EnvironmentType.ProductionTest, () => Texts.Global_EnvProductionTest) },
            { "ProdEnvironmentName", new EnvironmentTuple("InnerDevEnvironmentName", EnvironmentType.Production, () => Texts.Global_EnvProduction) },
        });

        public static EnvironmentTuple Current
        {
            get
            {
                string environmentKeyName = System.Configuration.ConfigurationManager.AppSettings.GetWithEnv(EnvironmentNameAppKey);

                return EnvDict.ContainsKey(environmentKeyName) ? EnvDict[environmentKeyName] : new EnvironmentTuple("unknown", EnvironmentType.Unknown, () => "unknown");
            }
        }
    }
}