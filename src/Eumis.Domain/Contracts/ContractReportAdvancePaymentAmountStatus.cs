using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportAdvancePaymentAmountStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportAdvancePaymentAmountStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportAdvancePaymentAmountStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}