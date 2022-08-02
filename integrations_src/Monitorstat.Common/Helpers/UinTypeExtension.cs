
using Eumis.Common.Config;
using Monitorstat.Common.Contracts;
using Monitorstat.Common.MonitorstatService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitorstat.Common.Helpers
{
    public static class UinTypeExtension
    {
        const string projectSettings = "Eumis.IntegrationMonitorstat.Host";

        static Func<string, Guid> getGuid = (s) => Guid.Parse(ConfigurationManager.AppSettings.GetWithEnv($"{projectSettings}:{s}"));
        static Func<string, string> getName = (s) => ConfigurationManager.AppSettings.GetWithEnv($"{projectSettings}:Name{s}");

        static Func<IdentifierType, string, (IdentifierType, string, Guid)> func = (t, u) => (t, getName(u), getGuid(u));

        static readonly IDictionary<UinType, (IdentifierType, string, Guid)> monitorstatUinMap = new Dictionary<UinType, (IdentifierType, string, Guid)>()
        {
            { UinType.Eik, func(IdentifierType.EIK, nameof(IdentifierType.EIK))},
            { UinType.Bulstat, func(IdentifierType.Bulstat, nameof(IdentifierType.Bulstat))},
            { UinType.PersonalBulstat, func(IdentifierType.EGN, nameof(IdentifierType.EGN))},
            { UinType.Foreign, func(IdentifierType.LNCh, nameof(IdentifierType.LNCh))},
        };

        public static IdentifierType ToMonitorstatType(this UinType uinType)
        {
            return monitorstatUinMap[uinType].Item1;    
        }


        public static Nomenclature ToMonitorstatNomenclature(this UinType uinType)
        {
            return new Nomenclature()
            {
                Id = monitorstatUinMap[uinType].Item3,
                Name = monitorstatUinMap[uinType].Item2,
            };
        }

        public static UinType ToEumisType(this IdentifierType identifierType)
        {
            return monitorstatUinMap
                .Where(x => x.Value.Item1 == identifierType)
                .Select(x => x.Key)
                .Single();
        }

    }
}
