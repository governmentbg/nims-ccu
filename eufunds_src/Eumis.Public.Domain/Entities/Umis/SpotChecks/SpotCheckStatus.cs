using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public enum SpotCheckStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведена")]
        Entered = 2
    }
}
