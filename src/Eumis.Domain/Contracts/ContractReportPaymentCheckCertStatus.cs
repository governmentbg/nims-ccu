using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportPaymentCheckCertStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentCheckCertStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentCheckCertStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}