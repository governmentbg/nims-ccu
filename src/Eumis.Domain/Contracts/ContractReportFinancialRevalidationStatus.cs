using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportFinancialRevalidationStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialRevalidationStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialRevalidationStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}