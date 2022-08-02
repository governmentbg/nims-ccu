using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportType
    {
        [Description("Авансово искане за плащане")]
        AdvancePayment = 1,

        [Description("Технически отчет")]
        Technical = 2,

        [Description("Искане за плащане, технически отчет, финансов отчет")]
        PaymentTechnicalFinancial = 3,

        [Description("Искане за плащане и финансов отчет")]
        PaymentFinancial = 4
    }
}