using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportIndicatorApproval
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportIndicatorApproval_Approved), ResourceType = typeof(DomainEnumTexts))]
        Approved = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportIndicatorApproval_Unapproved), ResourceType = typeof(DomainEnumTexts))]
        Unapproved = 2,
    }
}