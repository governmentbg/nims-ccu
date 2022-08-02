using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportPaymentStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведено")]
        Entered = 2,

        [Description("Актуално")]
        Actual = 3,

        [Description("Върнато")]
        Returned = 4
    }
}