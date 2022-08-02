using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportAdvancePaymentAmountCertStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Приключен")]
        Ended = 2
    }
}