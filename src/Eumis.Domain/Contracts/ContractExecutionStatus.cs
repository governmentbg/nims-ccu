using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractExecutionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractExecutionStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractExecutionStatus_Paused), ResourceType = typeof(DomainEnumTexts))]
        Paused = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractExecutionStatus_Monitored), ResourceType = typeof(DomainEnumTexts))]
        Monitored = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractExecutionStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractExecutionStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 5,

        [Description(Description = nameof(DomainEnumTexts.ContractExecutionStatus_Concluded), ResourceType = typeof(DomainEnumTexts))]
        Concluded = 6,

        [Description(Description = nameof(DomainEnumTexts.ContractExecutionStatus_Suspended), ResourceType = typeof(DomainEnumTexts))]
        Suspended = 7,
    }
}
