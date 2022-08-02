using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportCertAuthorityFinancialCorrectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityFinancialCorrectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityFinancialCorrectionStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}
