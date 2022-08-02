using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Приключен")]
        SentChecked = 3,

        [Description("В проверка")]
        Unchecked = 4,

        [Description("Приет")]
        Accepted = 5,

        [Description("Отхвърлен")]
        Refused = 6
    }
}