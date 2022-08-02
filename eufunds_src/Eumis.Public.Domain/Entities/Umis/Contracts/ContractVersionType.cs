using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractVersionType
    {
        [Description("Нов договор")]
        NewContract = 1,

        [Description("Изменение")]
        Annex = 2,

        [Description("Промяна")]
        Change = 3
    }
}
