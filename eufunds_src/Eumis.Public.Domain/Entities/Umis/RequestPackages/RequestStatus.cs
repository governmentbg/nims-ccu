using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.RequestPackages
{
    public enum RequestStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведена")]
        Entered = 2,

        [Description("Проверена")]
        Checked = 3,

        [Description("Активна")]
        Active = 4,

        [Description("Отхвърлена")]
        Rejected = 5,

        [Description("Анулирана")]
        Canceled = 6
    }
}
