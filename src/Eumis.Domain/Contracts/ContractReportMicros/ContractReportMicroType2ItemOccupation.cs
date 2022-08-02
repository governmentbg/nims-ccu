using Eumis.Common.Json;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemOccupation
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemOccupation_Employed), ResourceType = typeof(DomainEnumTexts))]
        Employed = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemOccupation_SelfEmployed), ResourceType = typeof(DomainEnumTexts))]
        SelfEmployed = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemOccupation_UnemployedLessThanSixMonths), ResourceType = typeof(DomainEnumTexts))]
        UnemployedLessThanSixMonths = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemOccupation_UnemployedMoreThanSixMonths), ResourceType = typeof(DomainEnumTexts))]
        UnemployedMoreThanSixMonths = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemOccupation_UnemployedMoreThanYear), ResourceType = typeof(DomainEnumTexts))]
        UnemployedMoreThanYear = 5,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemOccupation_Unemployed), ResourceType = typeof(DomainEnumTexts))]
        Unemployed = 6,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemOccupation_UnemployedWithoutEducation), ResourceType = typeof(DomainEnumTexts))]
        UnemployedWithoutEducation = 7,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemOccupation_Student), ResourceType = typeof(DomainEnumTexts))]
        Student = 8,
    }
}