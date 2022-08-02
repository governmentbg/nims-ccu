using Eumis.Common.Json;

namespace Eumis.Domain.Audits
{
    public enum AuditSubjectType
    {
        [Description(Description = nameof(DomainEnumTexts.AuditSubjectType_Beneficiary), ResourceType = typeof(DomainEnumTexts))]
        Beneficiary = 1,

        [Description(Description = nameof(DomainEnumTexts.AuditSubjectType_Partner), ResourceType = typeof(DomainEnumTexts))]
        Partner = 2,

        [Description(Description = nameof(DomainEnumTexts.AuditSubjectType_Executor), ResourceType = typeof(DomainEnumTexts))]
        Executor = 3,

        [Description(Description = nameof(DomainEnumTexts.AuditSubjectType_FinancialIntermediary), ResourceType = typeof(DomainEnumTexts))]
        FinancialIntermediary = 4,

        [Description(Description = nameof(DomainEnumTexts.AuditSubjectType_EndUser), ResourceType = typeof(DomainEnumTexts))]
        EndUser = 5,

        [Description(Description = nameof(DomainEnumTexts.AuditSubjectType_ManagingAuthority), ResourceType = typeof(DomainEnumTexts))]
        ManagingAuthority = 6,

        [Description(Description = nameof(DomainEnumTexts.AuditSubjectType_SO), ResourceType = typeof(DomainEnumTexts))]
        SO = 7,

        [Description(Description = nameof(DomainEnumTexts.AuditSubjectType_MZ), ResourceType = typeof(DomainEnumTexts))]
        MZ = 8,

        [Description(Description = nameof(DomainEnumTexts.AuditSubjectType_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 9,
    }
}
