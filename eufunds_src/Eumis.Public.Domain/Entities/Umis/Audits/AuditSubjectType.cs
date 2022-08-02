using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Audits
{
    public enum AuditSubjectType
    {
        [Description("Бенефициент")]
        Beneficiary = 1,

        [Description("Партньор")]
        Partner = 2,

        [Description("Изпълнител")]
        Executor = 3,

        [Description("Финансов посредник")]
        FinancialIntermediary = 4,

        [Description("Краен получател")]
        EndUser = 5,

        [Description("УО")]
        ManagingAuthority = 6,

        [Description("СО")]
        SO = 7,

        [Description("МЗ")]
        MZ = 8,

        [Description("Друг")]
        Other = 9
    }
}
