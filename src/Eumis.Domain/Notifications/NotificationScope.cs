using Eumis.Common.Json;

namespace Eumis.Domain.Notifications
{
    public enum NotificationScope
    {
        [Description(Description = nameof(DomainEnumTexts.NotificationScope_Programme), ResourceType = typeof(DomainEnumTexts))]
        Programme = 1,

        [Description(Description = nameof(DomainEnumTexts.NotificationScope_ProgrammePriority), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriority = 2,

        [Description(Description = nameof(DomainEnumTexts.NotificationScope_Procedure), ResourceType = typeof(DomainEnumTexts))]
        Procedure = 3,

        [Description(Description = nameof(DomainEnumTexts.NotificationScope_Contract), ResourceType = typeof(DomainEnumTexts))]
        Contract = 4,
    }
}
