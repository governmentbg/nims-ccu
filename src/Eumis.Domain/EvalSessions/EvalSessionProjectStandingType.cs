using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionProjectStandingType
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionProjectStandingType_Automatic), ResourceType = typeof(DomainEnumTexts))]
        Automatic = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionProjectStandingType_Manual), ResourceType = typeof(DomainEnumTexts))]
        Manual = 2,
    }
}
