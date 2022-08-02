using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
{
    public enum CorrectionBearer
    {
        [Description("Бенефициент")]
        Beneficiary = 1,

        [Description("УО")]
        AdministrativeAuthority = 2,

        [Description("УО и Бенефициент")]
        BeneficiaryAdminAuthority = 3,

        [Description("Национален бюджет")]
        NationalBudget = 4
    }
}