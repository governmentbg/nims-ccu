using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes
{
    public enum IndicativeAnnualWorkingProgrammeStatus
    {
        [LocalizableDescription("IndicativeAnnualWorkingProgrammeStatus_Draft")]
        Draft = 1,

        [LocalizableDescription("IndicativeAnnualWorkingProgrammeStatus_Published")]
        Published = 2,

        [LocalizableDescription("IndicativeAnnualWorkingProgrammeStatus_Archived")]
        Archived = 3,

        [LocalizableDescription("IndicativeAnnualWorkingProgrammeStatus_Canceled")]
        Canceled = 4,
    }
}
