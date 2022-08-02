using Eumis.Common.Json;

namespace Eumis.Domain.CertAuthorityChecks
{
    public enum CertAuthorityCheckSubjectType
    {
        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckSubjectType_ManagingAuthority), ResourceType = typeof(DomainEnumTexts))]
        ManagingAuthority = 1,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckSubjectType_MZ), ResourceType = typeof(DomainEnumTexts))]
        MZ = 2,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckSubjectType_Beneficiary), ResourceType = typeof(DomainEnumTexts))]
        Beneficiary = 3,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckSubjectType_EndUser), ResourceType = typeof(DomainEnumTexts))]
        EndUser = 4,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckSubjectType_FinancialIntermediary), ResourceType = typeof(DomainEnumTexts))]
        FinancialIntermediary = 5,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckSubjectType_Executants), ResourceType = typeof(DomainEnumTexts))]
        Executants = 6,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckSubjectType_Partners), ResourceType = typeof(DomainEnumTexts))]
        Partners = 7,
    }
}
