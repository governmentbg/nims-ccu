using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionReportProjectStandingStatus
    {
        [Description("Одобрено")]
        Approved = 1,

        [Description("Резерва")]
        Reserve = 2,

        [Description("Отхвърлено")]
        Rejected = 3,

        [Description("Отхвърлено на ОАСД")]
        RejectedAtASD = 4,

        [Description("Отхвърлено на ТФО")]
        RejectedAtTFO = 5,

        [Description("Оттеглено")]
        Withdrawed = 6,

        [Description("Анулирано")]
        Canceled = 7,

        [Description("Без класиране")]
        WithoutStanding = 8
    }
}
