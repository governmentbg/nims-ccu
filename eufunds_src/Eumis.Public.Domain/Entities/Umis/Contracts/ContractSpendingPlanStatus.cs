using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractSpendingPlanStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Актуален")]
        Active = 3,

        [Description("Архивиран")]
        Archived = 4
    }
}
