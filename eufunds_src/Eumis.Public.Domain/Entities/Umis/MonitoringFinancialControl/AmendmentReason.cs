using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
{
    public enum AmendmentReason
    {
        [Description("Съдебно решение")]
        CourtOrder = 1,

        [Description("Предложение на СО")]
        CertifyingAuthority = 2,

        [Description("Предложение на ОО")]
        АuditAuthority = 3,

        [Description("Предложение ЕК")]
        EuropeanCommission = 4,

        [Description("Предложение ЕСП")]
        EuropeanCourtAuditor = 5
    }
}