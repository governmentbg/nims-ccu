using Eumis.Common.Json;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType_Type1), ResourceType = typeof(DomainEnumTexts))]
        Type1 = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType_Type2), ResourceType = typeof(DomainEnumTexts))]
        Type2 = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType_Type3), ResourceType = typeof(DomainEnumTexts))]
        Type3 = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType_Type4), ResourceType = typeof(DomainEnumTexts))]
        Type4 = 4,
    }
}