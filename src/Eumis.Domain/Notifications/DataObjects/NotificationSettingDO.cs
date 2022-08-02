using System;

namespace Eumis.Domain.Notifications.DataObjects
{
    public class NotificationSettingDO
    {
        public NotificationSettingDO()
        {
        }

        public NotificationSettingDO(NotificationSetting setting)
        {
            this.NotificationSettingId = setting.NotificationSettingId;
            this.NotificationEventId = setting.NotificationEventId;
            this.ProgrammeId = setting.ProgrammeId;
            this.Scope = setting.Scope;
            this.Status = setting.Status;
            this.CreateDate = setting.CreateDate;
            this.ModifyDate = setting.ModifyDate;
            this.Version = setting.Version;
        }

        public int NotificationSettingId { get; set; }

        public int NotificationEventId { get; set; }

        public NotificationScope? Scope { get; set; }

        public int? ProgrammeId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public NotificationSettingStatus Status { get; set; }

        public byte[] Version { get; set; }
    }
}
