using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public enum UinType
    {
        [LocalizableDescription("UinType_Eik")]
        Eik = 0,

        [LocalizableDescription("UinType_Bulstat")]
        Bulstat = 1,

        [LocalizableDescription("UinType_Personal")]
        PersonalBulstat = 2,

        [LocalizableDescription("UinType_Foreign")]
        Foreign = 3
    }
}
