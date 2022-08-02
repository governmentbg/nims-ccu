using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.ReimbursedAmounts
{
    public enum Reimbursement
    {
        [Description("По банков път")]
        Bank = 1,

        [Description("Чрез прихващане")]
        Deduction = 2,

        [Description("Отписани")]
        WrittenОff = 3,

        [Description("Отнемане на разпределен лимит")]
        DistributedLimitDeduction = 4,
    }
}
