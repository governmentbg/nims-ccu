using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public enum SpotCheckType
    {
        [Description("Планирана")]
        Planned = 1,

        [Description("Непланирана")]
        NotPlanned = 2
    }
}
