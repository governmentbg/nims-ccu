using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl
{
    public enum CorrectionBearer
    {
        [Description(Description = nameof(DomainEnumTexts.CorrectionBearer_Beneficiary), ResourceType = typeof(DomainEnumTexts))]
        Beneficiary = 1,

        [Description(Description = nameof(DomainEnumTexts.CorrectionBearer_AdministrativeAuthority), ResourceType = typeof(DomainEnumTexts))]
        AdministrativeAuthority = 2,

        [Description(Description = nameof(DomainEnumTexts.CorrectionBearer_BeneficiaryAdminAuthority), ResourceType = typeof(DomainEnumTexts))]
        BeneficiaryAdminAuthority = 3,

        [Description(Description = nameof(DomainEnumTexts.CorrectionBearer_NationalBudget), ResourceType = typeof(DomainEnumTexts))]
        NationalBudget = 4,
    }
}