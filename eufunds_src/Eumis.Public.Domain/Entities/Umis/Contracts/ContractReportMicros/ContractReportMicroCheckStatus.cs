using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public enum ContractReportMicroCheckStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Актуална")]
        Active = 2,

        [Description("Архивирана")]
        Archived = 3
    }
}