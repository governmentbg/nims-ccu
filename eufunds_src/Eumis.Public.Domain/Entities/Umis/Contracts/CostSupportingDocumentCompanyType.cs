using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum CostSupportingDocumentCompanyType
    {
        [Description("Бенефициент")]
        Beneficiary = 1,

        [Description("Партньор")]
        Partner = 2,

        [Description("Изпълнител")]
        Contractor = 3
    }
}