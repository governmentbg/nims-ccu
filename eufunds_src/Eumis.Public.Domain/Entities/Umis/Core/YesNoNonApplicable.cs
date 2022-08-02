using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Core
{
    public enum YesNoNonApplicable
    {
        [Description("Да")]
        Yes = 1,

        [Description("Не")]
        No = 2,

        [Description("Не е приложимо")]
        NotApplicable = 3,
    }
}
