using System.ComponentModel;

namespace Eumis.Public.Web.Models.Company
{
    public enum CompanyEnumType
    {
        [Description("Бенефициент")]
        Beneficiary = 0,

        [Description("Партньор")]
        Partner = 1,

        [Description("Изпълнител")]
        Contractor = 2,

        [Description("Подизпълнител")]
        Subcontractor = 3,

        [Description("Член на обединението")]
        Member = 4,
    }
}