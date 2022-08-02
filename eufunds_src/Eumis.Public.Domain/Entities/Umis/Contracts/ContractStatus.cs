using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2
    }
}
