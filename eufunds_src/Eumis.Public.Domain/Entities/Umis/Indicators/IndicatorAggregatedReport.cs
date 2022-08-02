using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.Indicators
{
    public enum IndicatorAggregatedReport
    {
        [LocalizableDescriptionAttribute("IndicatorAggregatedReport_NotAggregated")]
        NotAggregated = 1,

        [LocalizableDescriptionAttribute("IndicatorAggregatedReport_Aggregated")]
        Aggregated = 2,

        [LocalizableDescriptionAttribute("IndicatorAggregatedReport_Inapplicable")]
        Inapplicable = 3,
    }
}
