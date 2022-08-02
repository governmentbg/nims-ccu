using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public enum SpotCheckAscertainmentType
    {
        [Description("Съществена")]
        Primary = 1,

        [Description("Второстепенна")]
        Secondary = 2,

        [Description("Несъществена")]
        Minor = 3,

        [Description("Информативна")]
        Info = 4
    }
}
