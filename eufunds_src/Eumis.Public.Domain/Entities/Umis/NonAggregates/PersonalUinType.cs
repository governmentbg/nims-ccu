using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public enum PersonalUinType
    {
        [Description("ЕГН")]
        PersonalBulstat = 1,

        [Description("ЛНЧ")]
        ForeignNumber = 2
    }
}
