using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportCertAuthorityCorrectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityCorrectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityCorrectionStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityCorrectionStatus_Deleted), ResourceType = typeof(DomainEnumTexts))]
        Deleted = 3,
    }
}
