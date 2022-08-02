using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportTechnicalCheckStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Актуална")]
        Active = 2,

        [Description("Архивирана")]
        Archived = 3
    }
}