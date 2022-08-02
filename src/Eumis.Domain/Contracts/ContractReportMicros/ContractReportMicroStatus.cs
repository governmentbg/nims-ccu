using Eumis.Common.Json;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public enum ContractReportMicroStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroStatus_Actual), ResourceType = typeof(DomainEnumTexts))]
        Actual = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroStatus_Returned), ResourceType = typeof(DomainEnumTexts))]
        Returned = 4,
    }
}