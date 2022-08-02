using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.FIReimbursedAmounts
{
    public enum FIReimbursementType
    {
        [Description("Главница")]
        ExGratia  = 1,

        [Description("Печалби")]
        NAP = 2,

        [Description("Други приходи и доходи")]
        Collateral = 3
    }
}