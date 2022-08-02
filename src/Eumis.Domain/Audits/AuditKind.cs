using Eumis.Common.Json;

namespace Eumis.Domain.Audits
{
    public enum AuditKind
    {
        [Description(Description = nameof(DomainEnumTexts.AuditKind_Planned), ResourceType = typeof(DomainEnumTexts))]
        Planned = 1,

        [Description(Description = nameof(DomainEnumTexts.AuditKind_NotPlanned), ResourceType = typeof(DomainEnumTexts))]
        NotPlanned = 2,
    }
}
