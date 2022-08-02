using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportCertAuthorityFinancialCorrectionCSDStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityFinancialCorrectionCSDStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityFinancialCorrectionCSDStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}
