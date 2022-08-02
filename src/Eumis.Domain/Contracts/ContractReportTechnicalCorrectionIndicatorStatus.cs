using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportTechnicalCorrectionIndicatorStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalCorrectionIndicatorStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalCorrectionIndicatorStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}