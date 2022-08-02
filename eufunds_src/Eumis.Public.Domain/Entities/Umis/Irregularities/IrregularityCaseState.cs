using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public enum IrregularityCaseState
    {
        [Description("Активен")]
        Active = 1,

        [Description("Приключен")]
        Ended = 2,

        [Description("Прекратен")]
        Terminated = 3,

        [Description("Отпаднал")]
        Canceled = 4
    }
}
