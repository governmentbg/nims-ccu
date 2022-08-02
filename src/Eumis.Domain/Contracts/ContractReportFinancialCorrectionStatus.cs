using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportFinancialCorrectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCorrectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCorrectionStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}