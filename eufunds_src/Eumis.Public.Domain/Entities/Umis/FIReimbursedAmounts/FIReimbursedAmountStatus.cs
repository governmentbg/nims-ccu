using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.FIReimbursedAmounts
{
    public enum FIReimbursedAmountStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведена")]
        Entered = 2,

        [Description("Анулирана")]
        Deleted = 3
    }
}
