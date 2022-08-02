using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportPaymentType
    {
        [Description("Авансово")]
        Advance = 1,

        [Description("Авансово, подлежащо на верификация съгласно чл. 131 от Регламент ЕС 1303/2013")]
        AdvanceVerification = 2,

        [Description("Междинно")]
        Intermediate = 3,

        [Description("Окончателно")]
        Final = 4
    }
}