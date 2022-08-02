using Eumis.Common.Json;
using Eumis.Domain.Notifications;
using Newtonsoft.Json;

namespace Eumis.Data.Notifications.ViewObjects
{
    public class NotificationSettingInfoVO
    {
        [JsonConverter(typeof(EnumDescriptionConverter))]
        public NotificationSettingStatus StatusDescr { get; set; }

        public NotificationSettingStatus Status { get; set; }

        public NotificationScope? Scope { get; set; }

        public byte[] Version { get; set; }
    }
}
