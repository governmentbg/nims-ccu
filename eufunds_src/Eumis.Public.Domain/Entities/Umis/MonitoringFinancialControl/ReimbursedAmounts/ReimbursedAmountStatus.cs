using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.ReimbursedAmounts
{
    public enum ReimbursedAmountStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведена")]
        Entered = 2,

        [Description("Анулирана")]
        Deleted = 3
    }
}
