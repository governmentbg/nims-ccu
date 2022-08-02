using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportAdvanceNVPaymentAmountApproval
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportAdvanceNVPaymentAmountApproval_Approved), ResourceType = typeof(DomainEnumTexts))]
        Approved = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportAdvanceNVPaymentAmountApproval_Unapproved), ResourceType = typeof(DomainEnumTexts))]
        Unapproved = 2,
    }
}