using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportRevalidationStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Анулиран")]
        Deleted = 3
    }
}
