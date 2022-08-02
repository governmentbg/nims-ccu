using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportRevalidationCertAuthorityFinancialCorrectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityFinancialCorrectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityFinancialCorrectionStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}
