using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportCorrectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportCorrectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCorrectionStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCorrectionStatus_Deleted), ResourceType = typeof(DomainEnumTexts))]
        Deleted = 3,
    }
}
