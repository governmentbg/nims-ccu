using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionStandpointStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Приключен")]
        Ended = 2,

        [Description("Анулиран")]
        Canceled = 3
    }
}