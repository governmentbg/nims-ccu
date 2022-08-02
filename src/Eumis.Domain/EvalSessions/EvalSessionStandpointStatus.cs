using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionStandpointStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionStandpointStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionStandpointStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionStandpointStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 3,
    }
}