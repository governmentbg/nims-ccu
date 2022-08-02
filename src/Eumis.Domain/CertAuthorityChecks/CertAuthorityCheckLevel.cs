using Eumis.Common.Json;

namespace Eumis.Domain.CertAuthorityChecks
{
    public enum CertAuthorityCheckLevel
    {
        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckLevel_Programme), ResourceType = typeof(DomainEnumTexts))]
        Programme = 1,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckLevel_ProgrammePriority), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriority = 2,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckLevel_Procedure), ResourceType = typeof(DomainEnumTexts))]
        Procedure = 3,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckLevel_Contract), ResourceType = typeof(DomainEnumTexts))]
        Contract = 4,
    }
}
