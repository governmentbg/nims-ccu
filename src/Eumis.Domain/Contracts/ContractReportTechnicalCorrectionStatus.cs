using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportTechnicalCorrectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalCorrectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalCorrectionStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalCorrectionStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}