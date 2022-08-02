using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportFinancialCSDBudgetItemStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCSDBudgetItemStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCSDBudgetItemStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}