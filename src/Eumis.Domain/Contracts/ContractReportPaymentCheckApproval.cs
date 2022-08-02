using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportPaymentCheckApproval
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentCheckApproval_Approved), ResourceType = typeof(DomainEnumTexts))]
        Approved = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentCheckApproval_Unapproved), ResourceType = typeof(DomainEnumTexts))]
        Unapproved = 2,
    }
}