using Eumis.Common.Json;
using Eumis.Domain.Notifications;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Notifications.ViewObjects
{
    public class NotificationSettingVO
    {
        public int NotificationSettingId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public NotificationScope? Scope { get; set; }

        public string EventName { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public NotificationSettingStatus Status { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
