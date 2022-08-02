using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionProjectStandingStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionProjectStandingStatus_Approved), ResourceType = typeof(DomainEnumTexts))]
        Approved = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionProjectStandingStatus_Reserve), ResourceType = typeof(DomainEnumTexts))]
        Reserve = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionProjectStandingStatus_Rejected), ResourceType = typeof(DomainEnumTexts))]
        Rejected = 3,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionProjectStandingStatus_RejectedAtASD), ResourceType = typeof(DomainEnumTexts))]
        RejectedAtASD = 4,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionProjectStandingStatus_RejectedAtTFO), ResourceType = typeof(DomainEnumTexts))]
        RejectedAtTFO = 5,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionProjectStandingStatus_RejectedAtPreliminary), ResourceType = typeof(DomainEnumTexts))]
        RejectedAtPreliminary = 6,
    }
}