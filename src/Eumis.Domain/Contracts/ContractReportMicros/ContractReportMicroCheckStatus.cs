using Eumis.Common.Json;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public enum ContractReportMicroCheckStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroCheckStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroCheckStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroCheckStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}