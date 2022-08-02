using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EuReimbursedAmounts
{
    public enum EuReimbursedAmountStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведена")]
        Entered = 2,

        [Description("Анулирана")]
        Removed = 3
    }
}
