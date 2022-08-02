using Eumis.Common.Json;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemCancelationReason
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemCancelationReason_JobOffer), ResourceType = typeof(DomainEnumTexts))]
        JobOffer = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemCancelationReason_Personal), ResourceType = typeof(DomainEnumTexts))]
        Personal = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemCancelationReason_Education), ResourceType = typeof(DomainEnumTexts))]
        Education = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemCancelationReason_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemCancelationReason_AddressChange), ResourceType = typeof(DomainEnumTexts))]
        AddressChange = 5,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemCancelationReason_EducationInstitutionChange), ResourceType = typeof(DomainEnumTexts))]
        EducationInstitutionChange = 6,
    }
}
