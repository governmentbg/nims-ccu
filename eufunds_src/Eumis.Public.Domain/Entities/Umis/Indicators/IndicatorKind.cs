using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.Indicators
{
    public enum IndicatorKind
    {
        [LocalizableDescriptionAttribute("IndicatorKind_Financial")]
        Financial = 1,

        [LocalizableDescriptionAttribute("IndicatorKind_Performance")]
        Performance = 2,

        [LocalizableDescriptionAttribute("IndicatorKind_Result")]
        Result = 3,

        [LocalizableDescriptionAttribute("IndicatorKind_PerformanceStage")]
        PerformanceStage = 4,
    }
}
