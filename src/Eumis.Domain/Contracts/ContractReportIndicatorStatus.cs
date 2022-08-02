using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportIndicatorStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportIndicatorStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportIndicatorStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}