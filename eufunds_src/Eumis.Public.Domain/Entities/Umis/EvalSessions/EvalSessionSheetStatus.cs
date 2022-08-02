using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionSheetStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Прекъснат")]
        Paused = 2,

        [Description("Приключен")]
        Ended = 3,

        [Description("Анулиран")]
        Canceled = 4
    }
}