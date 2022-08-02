using System;

namespace Eumis.Domain.Notifications
{
    public partial class UserNotification
    {
        public UserNotification()
        {
        }

        public UserNotification(int userId, int notificationEntryId)
        {
            var dateNow = DateTime.Now;

            this.UserId = userId;
            this.NotificationEntryId = notificationEntryId;
            this.IsRead = false;
            this.ModifyDate = dateNow;
            this.CreateDate = dateNow;
        }

        public void MarkAsRead()
        {
            this.IsRead = true;
            this.ModifyDate = DateTime.Now;
        }
    }
}
