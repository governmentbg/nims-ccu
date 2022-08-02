using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionResultStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionResultStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionResultStatus_Published), ResourceType = typeof(DomainEnumTexts))]
        Published = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionResultStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionResultStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 4,
    }
}
