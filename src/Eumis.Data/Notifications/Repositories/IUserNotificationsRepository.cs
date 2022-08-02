using Eumis.Data.Notifications.ViewObjects;
using Eumis.Domain.Notifications;
using System.Collections.Generic;

namespace Eumis.Data.Notifications.Repositories
{
    public interface IUserNotificationsRepository : IAggregateRepository<UserNotification>
    {
        List<UserNotificationVO> GetUserNotifications(int userId, int? notificationEventId, bool? isRead);

        int GetNewNotificationCount(int userId);

        UserNotificationVO GetUserNotification(int notificationId, int userId);
    }
}
