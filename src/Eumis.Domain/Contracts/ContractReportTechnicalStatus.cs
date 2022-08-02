using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportTechnicalStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalStatus_Actual), ResourceType = typeof(DomainEnumTexts))]
        Actual = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalStatus_Returned), ResourceType = typeof(DomainEnumTexts))]
        Returned = 4,
    }
}