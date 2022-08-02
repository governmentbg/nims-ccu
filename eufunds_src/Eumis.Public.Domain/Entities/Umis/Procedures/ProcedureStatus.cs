using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public enum ProcedureStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведена")]
        Entered = 2,

        [Description("Проверена")]
        Checked = 3,

        [Description("Активна")]
        Active = 4,

        [Description("Приключила")]
        Ended = 5,

        [Description("Прекратена")]
        Terminated = 6,

        [Description("Анулирана")]
        Canceled = 7
    }
}
