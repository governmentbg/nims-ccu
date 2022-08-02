using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public enum IrregularitySignalStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Активен")]
        Active = 2,

        [Description("Приключен")]
        Ended = 3,

        [Description("Анулиран")]
        Removed = 4
    }
}
