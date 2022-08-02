using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public enum SpotCheckAscertainmentStatus
    {
        [Description("Отворена")]
        Open = 1,

        [Description("Затворена")]
        Closed = 2,

        [Description("За информация")]
        Info = 3
    }
}
