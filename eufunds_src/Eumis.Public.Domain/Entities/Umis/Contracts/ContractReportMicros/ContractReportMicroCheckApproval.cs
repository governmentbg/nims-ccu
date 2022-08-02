using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros
{
    public enum ContractReportMicroCheckApproval
    {
        [Description("Одобрен")]
        Approved = 1,

        [Description("Неодобрен")]
        Unapproved = 2
    }
}