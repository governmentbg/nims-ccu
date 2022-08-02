using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.SapInterfaces
{
    public enum SapPaidAmountFund
    {
        [Description("ЕСФ")]
        ESF = 1,

        [Description("ЕФРР")]
        ERDF = 2,

        [Description("КФ")]
        CF = 3
    }
}
