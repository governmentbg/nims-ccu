using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportCorrectionStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Анулиран")]
        Deleted = 3
    }
}
