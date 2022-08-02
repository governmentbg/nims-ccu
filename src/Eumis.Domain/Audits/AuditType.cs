using Eumis.Common.Json;

namespace Eumis.Domain.Audits
{
    public enum AuditType
    {
        [Description(Description = nameof(DomainEnumTexts.AuditType_CourtOfAuditorsAudit), ResourceType = typeof(DomainEnumTexts))]
        CourtOfAuditorsAudit = 1,

        [Description(Description = nameof(DomainEnumTexts.AuditType_InternalAudit), ResourceType = typeof(DomainEnumTexts))]
        InternalAudit = 2,

        [Description(Description = nameof(DomainEnumTexts.AuditType_AdfiCheck), ResourceType = typeof(DomainEnumTexts))]
        AdfiCheck = 3,

        [Description(Description = nameof(DomainEnumTexts.AuditType_SystemAudit), ResourceType = typeof(DomainEnumTexts))]
        SystemAudit = 4,

        [Description(Description = nameof(DomainEnumTexts.AuditType_OperationsAudit), ResourceType = typeof(DomainEnumTexts))]
        OperationsAudit = 5,

        [Description(Description = nameof(DomainEnumTexts.AuditType_ThematicAudit), ResourceType = typeof(DomainEnumTexts))]
        ThematicAudit = 6,

        [Description(Description = nameof(DomainEnumTexts.AuditType_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 7,
    }
}
