using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Audits
{
    public enum AuditType
    {
        [Description("Одит от Сметна палата")]
        CourtОfAuditorsAudit = 1,

        [Description("Одит от Звеното за вътрешен одит")]
        InternalAudit = 2,

        [Description("Проверка от АДФИ")]
        AdfiCheck = 3,

        [Description("Системен одит")]
        SystemAudit = 4,

        [Description("Одит на операциите")]
        OperationsAudit = 5,

        [Description("Тематичен одит")]
        ThematicAudit = 6,

        [Description("Друг")]
        Other = 7
    }
}
