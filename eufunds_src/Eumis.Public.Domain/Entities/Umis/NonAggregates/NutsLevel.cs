using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public enum NutsLevel
    {
        [Description("Държава")]
        Country = 1,

        [Description("NUTS ниво 1")]
        RegionNUTS1 = 2,

        [Description("NUTS ниво 2")]
        RegionNUTS2 = 3,

        [Description("Област")]
        District = 4,

        [Description("Община")]
        Municipality = 5,

        [Description("Населено място")]
        Settlement = 6,

        [Description("Защитена зона")]
        ProtectedZone = 7
    }
}
