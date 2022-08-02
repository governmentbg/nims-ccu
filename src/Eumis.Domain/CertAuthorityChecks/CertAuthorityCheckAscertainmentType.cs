using Eumis.Common.Json;

namespace Eumis.Domain.CertAuthorityChecks
{
    public enum CertAuthorityCheckAscertainmentType
    {
        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckAscertainmentType_Primary), ResourceType = typeof(DomainEnumTexts))]
        Primary = 1,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckAscertainmentType_Secondary), ResourceType = typeof(DomainEnumTexts))]
        Secondary = 2,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckAscertainmentType_Minor), ResourceType = typeof(DomainEnumTexts))]
        Minor = 3,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckAscertainmentType_Informative), ResourceType = typeof(DomainEnumTexts))]
        Informative = 4,
    }
}
