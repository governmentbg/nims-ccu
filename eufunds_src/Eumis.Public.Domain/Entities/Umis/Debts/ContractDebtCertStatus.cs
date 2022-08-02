using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Debts
{
    public enum ContractDebtCertStatus
    {
        [Description("Да")]
        Yes = 1,

        [Description("Не")]
        No = 2,

        [Description("Частично")]
        Partial = 3
    }
}
