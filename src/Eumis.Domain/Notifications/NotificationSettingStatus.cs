using Eumis.Common.Json;

namespace Eumis.Domain.Notifications
{
    public enum NotificationSettingStatus
    {
        [Description(Description = nameof(DomainEnumTexts.NotificationSettingStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.NotificationSettingStatus_Actual), ResourceType = typeof(DomainEnumTexts))]
        Actual = 2,
    }
}
