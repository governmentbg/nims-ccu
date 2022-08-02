using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportAdvancePaymentAmountApproval
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportAdvancePaymentAmountApproval_Approved), ResourceType = typeof(DomainEnumTexts))]
        Approved = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportAdvancePaymentAmountApproval_Unapproved), ResourceType = typeof(DomainEnumTexts))]
        Unapproved = 2,
    }
}