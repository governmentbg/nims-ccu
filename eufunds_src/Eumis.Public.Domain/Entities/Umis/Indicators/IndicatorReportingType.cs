using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Indicators
{
    public enum IndicatorReportingType
    {
        [Description("Ръчно")]
        Manual = 1,

        [Description("Автоматично (сумиране)")]
        Automatic = 2,
    }
}
