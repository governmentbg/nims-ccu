using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.RequestPackages
{
    public enum RequestPackageStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Проверен")]
        Checked = 3,

        [Description("Приключен")]
        Ended = 4,

        [Description("Анулиран")]
        Canceled = 5
    }
}
