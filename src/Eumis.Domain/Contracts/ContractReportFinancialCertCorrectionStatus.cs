using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportFinancialCertCorrectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCertCorrectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCertCorrectionStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}