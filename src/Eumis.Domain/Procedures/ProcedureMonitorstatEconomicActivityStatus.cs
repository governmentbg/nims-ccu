using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureMonitorstatEconomicActivityStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureMonitorstatEconomicActivityStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureMonitorstatEconomicActivityStatus_Activated), ResourceType = typeof(DomainEnumTexts))]
        Activated = 2,
    }
}
