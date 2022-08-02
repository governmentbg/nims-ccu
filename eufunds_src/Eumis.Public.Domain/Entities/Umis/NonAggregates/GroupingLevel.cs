using Eumis.Public.Domain.Helpers;
using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public enum GroupingLevel
    {
        [LocalizableDescriptionAttribute("GroupingLevel_Programme")]
        Programme = 1,

        [LocalizableDescriptionAttribute("GroupingLevel_ProgrammePriority")]
        ProgrammePriority = 2,

        [LocalizableDescriptionAttribute("GroupingLevel_Procedure")]
        Procedure = 3,

        [LocalizableDescriptionAttribute("GroupingLevel_Contract")]
        Contract = 4,
    }
}
