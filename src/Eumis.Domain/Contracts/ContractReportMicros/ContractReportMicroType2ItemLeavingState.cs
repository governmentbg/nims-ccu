using Eumis.Common.Json;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemLeavingState
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemLeavingState_NoJobOfferOrEducation), ResourceType = typeof(DomainEnumTexts))]
        NoJobOfferOrEducation = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemLeavingState_Student), ResourceType = typeof(DomainEnumTexts))]
        Student = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemLeavingState_JobOfferOrEducation), ResourceType = typeof(DomainEnumTexts))]
        JobOfferOrEducation = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemLeavingState_EmployedFromTheSameEmployer), ResourceType = typeof(DomainEnumTexts))]
        EmployedFromTheSameEmployer = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemLeavingState_EmployedFromDifferentEmployer), ResourceType = typeof(DomainEnumTexts))]
        EmployedFromDifferentEmployer = 5,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemLeavingState_SelfEmployed), ResourceType = typeof(DomainEnumTexts))]
        SelfEmployed = 6,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemLeavingState_LeftOutOfEducation), ResourceType = typeof(DomainEnumTexts))]
        LeftOutOfEducation = 7,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemLeavingState_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 8,
    }
}
