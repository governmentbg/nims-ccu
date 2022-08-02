using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Debts
{
    public enum ContractDebtVersionStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Актуален")]
        Actual = 2,

        [Description("Архивиран")]
        Archived = 3
    }
}
