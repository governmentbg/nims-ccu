using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public enum YesNoOther
    {
        [Description("Да")]
        Yes = 1,

        [Description("Не")]
        No = 2,

        [Description("Друго")]
        Other = 3
    }
}
