using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureMonitorstatEconomicActivityType
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureMonitorstatEconomicActivityType_Main), ResourceType = typeof(DomainEnumTexts))]
        Main = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureMonitorstatEconomicActivityType_MainAndAdditional), ResourceType = typeof(DomainEnumTexts))]
        MainAndAdditional = 2,
    }
}
