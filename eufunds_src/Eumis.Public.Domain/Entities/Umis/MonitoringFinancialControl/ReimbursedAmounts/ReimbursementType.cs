using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.ReimbursedAmounts
{
    public enum ReimbursementType
    {
        [Description("Доброволно възстановяване")]
        ExGratia  = 1,

        [Description("Събиране чрез НАП")]
        NAP = 2,

        [Description("Събиране чрез активиране на обезпечения (банкова гаранция)")]
        Collateral = 3,

        [Description("Друго")]
        Other = 4
    }
}
