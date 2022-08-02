using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections
{
    public enum FlatFinancialCorrectionType
    {
        [Description(Description = nameof(DomainEnumTexts.FlatFinancialCorrectionType_AdministrativeAuthority), ResourceType = typeof(DomainEnumTexts))]
        AdministrativeAuthority = 1,

        [Description(Description = nameof(DomainEnumTexts.FlatFinancialCorrectionType_CertifyingAuthority), ResourceType = typeof(DomainEnumTexts))]
        CertifyingAuthority = 2,

        [Description(Description = nameof(DomainEnumTexts.FlatFinancialCorrectionType_AuditAuthority), ResourceType = typeof(DomainEnumTexts))]
        AuditAuthority = 3,

        [Description(Description = nameof(DomainEnumTexts.FlatFinancialCorrectionType_EuropeanCommission), ResourceType = typeof(DomainEnumTexts))]
        EuropeanCommission = 4,
    }
}