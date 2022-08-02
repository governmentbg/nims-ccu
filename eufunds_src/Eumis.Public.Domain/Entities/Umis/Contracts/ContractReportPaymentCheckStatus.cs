using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportPaymentCheckStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Актуално")]
        Active = 2,

        [Description("Архивирано")]
        Archived = 3
    }
}