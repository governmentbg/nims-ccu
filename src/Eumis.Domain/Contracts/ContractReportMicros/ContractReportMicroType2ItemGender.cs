using Eumis.Common.Json;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemGender
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemGender_Male), ResourceType = typeof(DomainEnumTexts))]
        Male = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemGender_Female), ResourceType = typeof(DomainEnumTexts))]
        Female = 2,
    }
}