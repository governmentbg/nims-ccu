using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.Indicators
{
    public enum IndicatorAggregatedTarget
    {
        [LocalizableDescriptionAttribute("IndicatorAggregatedTarget_NotAggregated")]
        NotAggregated = 1,

        [LocalizableDescriptionAttribute("IndicatorAggregatedTarget_Aggregated")]
        Aggregated = 2,

        [LocalizableDescriptionAttribute("IndicatorAggregatedTarget_Inapplicable")]
        Inapplicable = 3,
    }
}
