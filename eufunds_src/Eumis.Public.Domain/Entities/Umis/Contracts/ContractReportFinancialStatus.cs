using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportFinancialStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Актуален")]
        Actual = 3,

        [Description("Върнат")]
        Returned = 4
    }
}