using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionUserStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionUserStatus_NotActivated), ResourceType = typeof(DomainEnumTexts))]
        NotActivated = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionUserStatus_Activated), ResourceType = typeof(DomainEnumTexts))]
        Activated = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionUserStatus_Deactivated), ResourceType = typeof(DomainEnumTexts))]
        Deactivated = 3,
    }
}
