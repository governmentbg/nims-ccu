using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.FinancialCorrections
{
    public enum FinancialCorrectionVersionStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Актуален")]
        Actual = 2,

        [Description("Архивиран")]
        Archived = 3
    }
}