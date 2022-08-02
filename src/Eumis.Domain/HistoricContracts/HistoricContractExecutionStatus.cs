using Eumis.Common.Json;

namespace Eumis.Domain.HistoricContracts
{
    public enum HistoricContractExecutionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.HistoricContractExecutionStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 1,

        [Description(Description = nameof(DomainEnumTexts.HistoricContractExecutionStatus_Paused), ResourceType = typeof(DomainEnumTexts))]
        Paused = 2,

        [Description(Description = nameof(DomainEnumTexts.HistoricContractExecutionStatus_Monitored), ResourceType = typeof(DomainEnumTexts))]
        Monitored = 3,

        [Description(Description = nameof(DomainEnumTexts.HistoricContractExecutionStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 4,

        [Description(Description = nameof(DomainEnumTexts.HistoricContractExecutionStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 5,

        [Description(Description = nameof(DomainEnumTexts.HistoricContractExecutionStatus_Concluded), ResourceType = typeof(DomainEnumTexts))]
        Concluded = 6,

        [Description(Description = nameof(DomainEnumTexts.HistoricContractExecutionStatus_Suspended), ResourceType = typeof(DomainEnumTexts))]
        Suspended = 7,
    }
}
