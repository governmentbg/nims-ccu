using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
{
    public enum PaymentReason
    {
        [Description("Авансово плащане")]
        AdvancePayment = 1,

        [Description("Верифицирани разходи по ново искане за плащане")]
        ApprovedExpenses = 2,

        [Description("Възстановени суми")]
        ReimbursedExpenses = 3,

        [Description("Доплащане")]
        AdditionalPayment = 4
    }
}