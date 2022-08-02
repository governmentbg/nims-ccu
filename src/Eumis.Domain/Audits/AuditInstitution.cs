using Eumis.Common.Json;

namespace Eumis.Domain.Audits
{
    public enum AuditInstitution
    {
        [Description(Description = nameof(DomainEnumTexts.AuditInstitution_CourtOfAuditors), ResourceType = typeof(DomainEnumTexts))]
        CourtOfAuditors = 1,

        [Description(Description = nameof(DomainEnumTexts.AuditInstitution_Internal), ResourceType = typeof(DomainEnumTexts))]
        Internal = 2,

        [Description(Description = nameof(DomainEnumTexts.AuditInstitution_Adfi), ResourceType = typeof(DomainEnumTexts))]
        Adfi = 3,

        [Description(Description = nameof(DomainEnumTexts.AuditInstitution_OO), ResourceType = typeof(DomainEnumTexts))]
        OO = 4,

        [Description(Description = nameof(DomainEnumTexts.AuditInstitution_EK), ResourceType = typeof(DomainEnumTexts))]
        EK = 5,

        [Description(Description = nameof(DomainEnumTexts.AuditInstitution_EuropeanCourtOfAuditors), ResourceType = typeof(DomainEnumTexts))]
        EuropeanCourtOfAuditors = 6,

        [Description(Description = nameof(DomainEnumTexts.AuditInstitution_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 7,
    }
}
