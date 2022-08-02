using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportFinancialRevalidationCSDStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialRevalidationCSDStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialRevalidationCSDStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}