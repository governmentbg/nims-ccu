using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportFinancialRevalidationCSDCertStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialRevalidationCSDCertStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialRevalidationCSDCertStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}