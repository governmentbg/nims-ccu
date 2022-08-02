using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}
