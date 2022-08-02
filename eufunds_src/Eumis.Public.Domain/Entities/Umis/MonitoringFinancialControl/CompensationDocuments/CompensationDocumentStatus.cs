using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.CompensationDocuments
{
    public enum CompensationDocumentStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведен")]
        Entered = 2,

        [Description("Анулиран")]
        Deleted = 3
    }
}
