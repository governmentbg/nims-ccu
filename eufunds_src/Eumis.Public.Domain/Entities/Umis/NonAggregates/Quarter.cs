using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public enum Quarter
    {
        [Description("Първо")]
        Q1 = 1,

        [Description("Второ")]
        Q2 = 2,

        [Description("Трето")]
        Q3 = 3,

        [Description("Четвърто")]
        Q4 = 4
    }
}
