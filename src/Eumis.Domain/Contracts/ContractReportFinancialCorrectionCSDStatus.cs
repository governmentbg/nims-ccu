using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportFinancialCorrectionCSDStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCorrectionCSDStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCorrectionCSDStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}