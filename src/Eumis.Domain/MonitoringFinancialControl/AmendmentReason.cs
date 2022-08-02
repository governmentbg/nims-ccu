using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl
{
    public enum AmendmentReason
    {
        [Description(Description = nameof(DomainEnumTexts.AmendmentReason_CourtOrder), ResourceType = typeof(DomainEnumTexts))]
        CourtOrder = 1,

        [Description(Description = nameof(DomainEnumTexts.AmendmentReason_CertifyingAuthority), ResourceType = typeof(DomainEnumTexts))]
        CertifyingAuthority = 2,

        [Description(Description = nameof(DomainEnumTexts.AmendmentReason_AuditAuthority), ResourceType = typeof(DomainEnumTexts))]
        AuditAuthority = 3,

        [Description(Description = nameof(DomainEnumTexts.AmendmentReason_EuropeanCommission), ResourceType = typeof(DomainEnumTexts))]
        EuropeanCommission = 4,

        [Description(Description = nameof(DomainEnumTexts.AmendmentReason_EuropeanCourtAuditor), ResourceType = typeof(DomainEnumTexts))]
        EuropeanCourtAuditor = 5,

        [Description(Description = nameof(DomainEnumTexts.AmendmentReason_ManagingAuthority), ResourceType = typeof(DomainEnumTexts))]
        ManagingAuthority = 6,

        [Description(Description = nameof(DomainEnumTexts.AmendmentReason_TechnicalError), ResourceType = typeof(DomainEnumTexts))]
        TechnicalError = 7,
    }
}
