using Eumis.Common.Json;

namespace Eumis.Domain.Debts
{
    public enum ContractDebtExecutionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractDebtExecutionStatus_VoluntaryRepaymentPeriod), ResourceType = typeof(DomainEnumTexts))]
        VoluntaryRepaymentPeriod = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractDebtExecutionStatus_InvoluntaryRepaymentProcess), ResourceType = typeof(DomainEnumTexts))]
        InvoluntaryRepaymentProcess = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractDebtExecutionStatus_InvoluntaryRepaymentProcessByNAP), ResourceType = typeof(DomainEnumTexts))]
        InvoluntaryRepaymentProcessByNAP = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractDebtExecutionStatus_FullyRepayed), ResourceType = typeof(DomainEnumTexts))]
        FullyRepayed = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractDebtExecutionStatus_Closed), ResourceType = typeof(DomainEnumTexts))]
        Closed = 5,

        [Description(Description = nameof(DomainEnumTexts.ContractDebtExecutionStatus_Irrecoverable), ResourceType = typeof(DomainEnumTexts))]
        Irrecoverable = 6,

        [Description(Description = nameof(DomainEnumTexts.ContractDebtExecutionStatus_Rescheduled), ResourceType = typeof(DomainEnumTexts))]
        Rescheduled = 7,

        [Description(Description = nameof(DomainEnumTexts.ContractDebtExecutionStatus_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 8,
    }
}
