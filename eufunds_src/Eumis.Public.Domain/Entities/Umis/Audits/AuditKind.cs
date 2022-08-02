using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Audits
{
    public enum AuditKind
    {
        [Description("Планиран")]
        Planned = 1,

        [Description("Извънреден")]
        NotPlanned = 2
    }
}
