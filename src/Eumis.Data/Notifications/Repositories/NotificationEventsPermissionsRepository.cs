using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Notifications;
using Eumis.Domain.Users;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Notifications.Repositories
{
    internal class NotificationEventsPermissionsRepository : Repository, INotificationEventsPermissionsRepository
    {
        private static readonly ConcurrentDictionary<int, List<(Type, string)>> StoredEventPermissions = new ConcurrentDictionary<int, List<(Type, string)>>();

        public NotificationEventsPermissionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public int[] GetAvailableNotificationEventIds(int userId)
        {
            List<int> availableEvents = new List<int>();

            var userPermissions = this.unitOfWork.DbContext.Set<UserPermission>().Where(x => x.UserId == userId).ToList();

            var events = this.unitOfWork.DbContext.Set<NotificationEvent>().Where(e => e.NotificationEventId != 1).ToList();
            events.ForEach(
                x =>
                {
                    var permissions = this.GetNotificationEventPermissions(x.NotificationEventId, x.IsProgrammeDependent);
                    permissions.ForEach(
                        z =>
                        {
                            if (userPermissions.Where(m => m.GetType() == z.Item1 && m.PermissionString == z.Item2).Any() && !availableEvents.Contains(x.NotificationEventId))
                            {
                                availableEvents.Add(x.NotificationEventId);
                            }
                        });
                });

            return availableEvents.ToArray();
        }

        public List<int> UsersHasNotificationEventPermission(List<int> subscribedUsers, NotificationEntry notificationEntry, bool isProgrammeDependent)
        {
            List<int> users = new List<int>();

            List<(Type, string)> storedPermissions = this.GetNotificationEventPermissions(notificationEntry.NotificationEventId, isProgrammeDependent);

            if (isProgrammeDependent)
            {
                var usersPemissionSet = this.unitOfWork.DbContext.Set<ProgrammePermission>()
                .Where(x => subscribedUsers.Contains(x.UserId))
                .ToList();

                foreach (var eventPermission in storedPermissions)
                {
                    var matchingUsers = usersPemissionSet.Where(x => x.GetType() == eventPermission.Item1 && eventPermission.Item2 == x.PermissionString && notificationEntry.ProgrammeId == x.ProgrammeId).Select(x => x.UserId).ToList();
                    users.AddRange(matchingUsers);
                }
            }
            else
            {
                var permissionSet = this.unitOfWork.DbContext.Set<CommonPermission>()
               .Where(x => subscribedUsers.Contains(x.UserId))
               .ToList();

                foreach (var eventPermission in storedPermissions)
                {
                    var matchingUsers = permissionSet.Where(x => x.GetType() == eventPermission.Item1 && eventPermission.Item2 == x.PermissionString).Select(x => x.UserId).ToList();
                    users.AddRange(matchingUsers);
                }
            }

            return users.Distinct().ToList();
        }

        private List<(Type, string)> GetNotificationEventPermissions(int notificationEventId, bool isProgrammeDependent)
        {
            List<(Type, string)> storedEventPermissions;

            if (!StoredEventPermissions.TryGetValue(notificationEventId, out storedEventPermissions))
            {
                var programmeEventsPermissions = (from nep in this.unitOfWork.DbContext.Set<NotificationEventPermission>().Where(z => z.NotificationEventId == notificationEventId)
                                                  join ne in this.unitOfWork.DbContext.Set<NotificationEvent>() on nep.NotificationEventId equals ne.NotificationEventId
                                                  select nep).ToList();

                List<(Type, string)> list = new List<(Type, string)>();
                foreach (var eventPermission in programmeEventsPermissions)
                {
                    Type enumPermissionType;
                    if (isProgrammeDependent)
                    {
                        enumPermissionType = User.ProgrammePermissionTypes[$"{eventPermission.PermissionType}Permissions"];
                    }
                    else
                    {
                        enumPermissionType = User.CommonPermissionTypes[$"{eventPermission.PermissionType}Permissions"];
                    }

                    var entityPermissionType = UserPermission.GetPermissionEntityType(enumPermissionType);
                    list.Add((entityPermissionType, eventPermission.Permission));
                }

                StoredEventPermissions.TryAdd(notificationEventId, list);
            }

            return StoredEventPermissions[notificationEventId];
        }
    }
}
