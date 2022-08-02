using Eumis.Common.Json;

namespace Eumis.Domain.ActionLogs
{
    public enum ActionLogType
    {
        [Description(Description = nameof(DomainEnumTexts.ActionLogType_Internal), ResourceType = typeof(DomainEnumTexts))]
        Internal = 1,

        [Description(Description = nameof(DomainEnumTexts.ActionLogType_Portal), ResourceType = typeof(DomainEnumTexts))]
        Portal = 2,

        [Description(Description = nameof(DomainEnumTexts.ActionLogType_UnsuccessfulLogin), ResourceType = typeof(DomainEnumTexts))]
        UnsuccessfulLogin = 3,
    }
}
