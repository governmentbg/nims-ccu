using Eumis.Common.Json;

namespace Eumis.Domain.CertAuthorityChecks
{
    public enum CertAuthorityCheckStatus
    {
        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckStatus_Removed), ResourceType = typeof(DomainEnumTexts))]
        Removed = 3,
    }
}
