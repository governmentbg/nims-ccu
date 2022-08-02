using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.SapInterfaces
{
    public enum SapPaymentType
    {
        [Description("Авансово")]
        Advance = 1,

        [Description("Междинно")]
        Intermediate = 2,

        [Description("Окончателно")]
        Final = 3,

        [Description("Глоба")]
        Fine = 4,

        [Description("Лихва")]
        Interest = 5,

        [Description("Възстановяване при доброволно прекратяване")]
        VoluntaryReimbursement = 6,

        [Description("Възстановяване при грешка")]
        MistakeReimbursement = 7,

        [Description("Възстановяване при нередност")]
        IrregularityReimbursement = 8,

        [Description("Банкова гаранция")]
        Bank = 9,

        [Description("Касов трансфер")]
        Transfer = 10
    }
}
