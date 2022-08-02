using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.Indicators
{
    public enum IndicatorType
    {
        [LocalizableDescriptionAttribute("IndicatorType_Specific")]
        Specific = 1,

        [LocalizableDescriptionAttribute("IndicatorType_Common")]
        Common = 2,

        [LocalizableDescriptionAttribute("IndicatorType_IndividualForProcedure")]
        IndividualForProcedure = 3,
    }
}
