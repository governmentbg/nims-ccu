using Eumis.Common.Json;

namespace Eumis.Domain.CertAuthorityChecks
{
    public enum CertAuthorityCheckKind
    {
        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckKind_BeneficiaryOrFinancialIntermediaryOrEndUser), ResourceType = typeof(DomainEnumTexts))]
        BeneficiaryOrFinancialIntermediaryOrEndUser = 1,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckKind_ProceduresAndControlActivitiesVerificationOfManagingAuthority), ResourceType = typeof(DomainEnumTexts))]
        ProceduresAndControlActivitiesVerificationOfManagingAuthority = 2,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckKind_ContractDebt), ResourceType = typeof(DomainEnumTexts))]
        ContractDebt = 3,
    }
}
