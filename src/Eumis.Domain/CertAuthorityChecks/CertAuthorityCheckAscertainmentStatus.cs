using Eumis.Common.Json;

namespace Eumis.Domain.CertAuthorityChecks
{
    public enum CertAuthorityCheckAscertainmentStatus
    {
        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckAscertainmentStatus_Open), ResourceType = typeof(DomainEnumTexts))]
        Open = 1,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckAscertainmentStatus_Closed), ResourceType = typeof(DomainEnumTexts))]
        Closed = 2,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckAscertainmentStatus_Informative), ResourceType = typeof(DomainEnumTexts))]
        Informative = 3,
    }
}
