using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionStandingStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionStandingStatus_Applied), ResourceType = typeof(DomainEnumTexts))]
        Applied = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionStandingStatus_Refused), ResourceType = typeof(DomainEnumTexts))]
        Refused = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionStandingStatus_AppliedWithChanges), ResourceType = typeof(DomainEnumTexts))]
        AppliedWithChanges = 3,
    }
}