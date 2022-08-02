using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
{
    public enum FlatFinancialCorrectionType
    {
        [Description("ФКСП, наложена от УО")]
        AdministrativeAuthority = 1,

        [Description("ФКСП, наложена във връзка с препоръка на СО")]
        CertifyingAuthority = 2,

        [Description("ФКСП, наложена във връзка с препоръка на ОО")]
        АuditAuthority = 3,

        [Description("ФКСП, наложена във връзка с препоръка на ЕК")]
        EuropeanCommission = 4
    }
}