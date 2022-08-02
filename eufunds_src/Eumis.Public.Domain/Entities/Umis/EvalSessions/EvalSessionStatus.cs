using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Активна")]
        Active = 2,

        [Description("Приключена")]
        Ended = 3,

        [Description("Анулирана")]
        Canceled = 4
    }
}
