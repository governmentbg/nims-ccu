using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.FinancialCorrections
{
    public enum FinancialCorrectionVersionViolationFoundBy
    {
        [Description("УО")]
        AdministrativeAuthority = 1,

        [Description("СО")]
        CertifyingAuthority = 2,

        [Description("ОО")]
        AuditAuthority = 3,

        [Description("ЕК")]
        EuropeanComission = 4,

        [Description("ЕСП")]
        EuropeanCourtОfAuditors = 5
    }
}