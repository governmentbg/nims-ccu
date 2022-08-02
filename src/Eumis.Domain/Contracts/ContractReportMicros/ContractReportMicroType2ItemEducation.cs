using Eumis.Common.Json;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemEducation
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemEducation_NoEducation), ResourceType = typeof(DomainEnumTexts))]
        NoEducation = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemEducation_ElementaryEducation), ResourceType = typeof(DomainEnumTexts))]
        ElementaryEducation = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemEducation_SecondaryEducation), ResourceType = typeof(DomainEnumTexts))]
        SecondaryEducation = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemEducation_PostSecondaryEducation), ResourceType = typeof(DomainEnumTexts))]
        PostSecondaryEducation = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemEducation_HigherEducation), ResourceType = typeof(DomainEnumTexts))]
        HigherEducation = 5,
    }
}
