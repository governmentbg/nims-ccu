using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.SpotChecks
{
    public enum SpotCheckTargetType
    {
        [Description("Бенефициент")]
        Beneficiary = 1,

        [Description("Изпълнител")]
        Executor = 2,

        [Description("Краен получател")]
        EndUser = 3,

        [Description("Партньор")]
        Partner = 4
    }
}
