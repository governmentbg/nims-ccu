using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
{
    public enum FlatFinancialCorrectionStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Актуална")]
        Actual = 2
    }
}