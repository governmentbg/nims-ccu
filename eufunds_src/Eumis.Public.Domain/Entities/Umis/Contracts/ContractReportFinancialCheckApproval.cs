using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractReportFinancialCheckApproval
    {
        [Description("Одобрен")]
        Approved = 1,

        [Description("Неодобрен")]
        Unapproved = 2
    }
}