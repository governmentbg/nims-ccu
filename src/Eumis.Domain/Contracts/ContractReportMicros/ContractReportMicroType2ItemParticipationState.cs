using Eumis.Common.Json;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public enum ContractReportMicroType2ItemParticipationState
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemParticipationState_LeftBeforeTheEnd), ResourceType = typeof(DomainEnumTexts))]
        LeftBeforeTheEnd = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemParticipationState_StayedUntilTheEnd), ResourceType = typeof(DomainEnumTexts))]
        StayedUntilTheEnd = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemParticipationState_HasCertificate), ResourceType = typeof(DomainEnumTexts))]
        HasCertificate = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemParticipationState_NoCertificate), ResourceType = typeof(DomainEnumTexts))]
        NoCertificate = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroType2ItemParticipationState_NoCertificateAtAll), ResourceType = typeof(DomainEnumTexts))]
        NoCertificateAtAll = 5,
    }
}
