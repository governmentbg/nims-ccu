using Eumis.Common.Json;

namespace Eumis.Domain.CertAuthorityChecks
{
    public enum CertAuthorityCheckType
    {
        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckType_Planned), ResourceType = typeof(DomainEnumTexts))]
        Planned = 1,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckType_NotPlanned), ResourceType = typeof(DomainEnumTexts))]
        NotPlanned = 2,
    }
}
