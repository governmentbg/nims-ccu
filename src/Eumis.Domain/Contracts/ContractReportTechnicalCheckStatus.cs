using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportTechnicalCheckStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalCheckStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalCheckStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalCheckStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}