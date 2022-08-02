using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public enum CertAuthorityCheckSubjectType
    {
        [Description("УО")]
        ManagingAuthority = 1,

        [Description("МЗ")]
        MZ = 2,

        [Description("Бенефициент")]
        Beneficiary = 3,

        [Description("Краен получател")]
        EndUser = 4,

        [Description("Финансов посредник")]
        FinancialIntermediary = 5,

        [Description("Изпълнители")]
        Executants = 6,

        [Description("Партньори")]
        Partners = 7
    }
}
