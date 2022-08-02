using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportFinancialCertCorrectionCSDStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCertCorrectionCSDStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCertCorrectionCSDStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}