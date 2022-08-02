using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportCertCorrectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportCertCorrectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertCorrectionStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertCorrectionStatus_Deleted), ResourceType = typeof(DomainEnumTexts))]
        Deleted = 3,
    }
}
