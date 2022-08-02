using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportAdvanceNVPaymentAmountStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportAdvanceNVPaymentAmountStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportAdvanceNVPaymentAmountStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}