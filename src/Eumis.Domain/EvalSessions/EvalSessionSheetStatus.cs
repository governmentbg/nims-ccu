using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionSheetStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionSheetStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionSheetStatus_Paused), ResourceType = typeof(DomainEnumTexts))]
        Paused = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionSheetStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 3,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionSheetStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 4,
    }
}