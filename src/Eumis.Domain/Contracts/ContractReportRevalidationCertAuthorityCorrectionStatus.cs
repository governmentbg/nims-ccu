using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportRevalidationCertAuthorityCorrectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityCorrectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityCorrectionStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityCorrectionStatus_Deleted), ResourceType = typeof(DomainEnumTexts))]
        Deleted = 3,
    }
}
