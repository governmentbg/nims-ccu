using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public enum CertAuthorityCheckKind
    {
        [Description("При бенефициент / финансов посредник / краен получател")]
        BeneficiaryOrFinancialIntermediaryOrEndUser = 1,

        [Description("Контрол на качеството в УО")]
        ManagingAuthorityQC = 2,

        [Description("Проверка на Книга на длъжниците")]
        ContractDebt = 3,

        [Description("Регулярна проверка на проведените процедури за избор на изпълнител с верифицирани разходи")]
        ProcurementsCheck = 4
    }
}
