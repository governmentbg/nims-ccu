using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportRevalidationCertStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}