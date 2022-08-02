using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EuReimbursedAmounts
{
    public enum EuReimbursedAmountPaymentType
    {
        [Description("Авансово финансиране")]
        Advance = 1,

        [Description("Междинно плащане")]
        Intermediate = 2,

        [Description("Плащане на окончателно салдо")]
        Final = 3
    }
}
