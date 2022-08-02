using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportAdvancePaymentAmountCertStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportAdvancePaymentAmountCertStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportAdvancePaymentAmountCertStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}