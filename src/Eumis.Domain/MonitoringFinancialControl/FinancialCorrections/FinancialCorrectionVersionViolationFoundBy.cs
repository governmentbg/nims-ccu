using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.FinancialCorrections
{
    public enum FinancialCorrectionVersionViolationFoundBy
    {
        [Description(Description = nameof(DomainEnumTexts.FinancialCorrectionVersionViolationFoundBy_AdministrativeAuthority), ResourceType = typeof(DomainEnumTexts))]
        AdministrativeAuthority = 1,

        [Description(Description = nameof(DomainEnumTexts.FinancialCorrectionVersionViolationFoundBy_CertifyingAuthority), ResourceType = typeof(DomainEnumTexts))]
        CertifyingAuthority = 2,

        [Description(Description = nameof(DomainEnumTexts.FinancialCorrectionVersionViolationFoundBy_AuditAuthority), ResourceType = typeof(DomainEnumTexts))]
        AuditAuthority = 3,

        [Description(Description = nameof(DomainEnumTexts.FinancialCorrectionVersionViolationFoundBy_EuropeanComission), ResourceType = typeof(DomainEnumTexts))]
        EuropeanComission = 4,

        [Description(Description = nameof(DomainEnumTexts.FinancialCorrectionVersionViolationFoundBy_EuropeanCourtOfAuditors), ResourceType = typeof(DomainEnumTexts))]
        EuropeanCourtOfAuditors = 5,
    }
}