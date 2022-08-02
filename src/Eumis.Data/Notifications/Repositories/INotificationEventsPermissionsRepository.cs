using Eumis.Data.Core;
using Eumis.Domain.Notifications;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Notifications.Repositories
{
    public interface INotificationEventsPermissionsRepository : IRepository
    {
        int[] GetAvailableNotificationEventIds(int userId);

        List<int> UsersHasNotificationEventPermission(List<int> subscribedUsers, NotificationEntry notificationEntry, bool isProgrammeDependent);
    }
}
