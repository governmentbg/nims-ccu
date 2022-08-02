using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.Indicators
{
    public enum IndicatorTrend
    {
        [LocalizableDescriptionAttribute("IndicatorTrend_Reduction")]
        Reduction = 1,

        [LocalizableDescriptionAttribute("IndicatorTrend_Increase")]
        Increase = 2,

        [LocalizableDescriptionAttribute("IndicatorTrend_Inapplicable")]
        Inapplicable = 3,
    }
}
