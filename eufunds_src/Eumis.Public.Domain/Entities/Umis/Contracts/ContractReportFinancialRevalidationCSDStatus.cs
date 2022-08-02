using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportFinancialRevalidationCSDStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Приключен")]
        Ended = 2
    }
}