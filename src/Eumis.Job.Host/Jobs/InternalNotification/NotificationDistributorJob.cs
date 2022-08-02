using Eumis.Common.Config;
using Eumis.Common.Db;
using Eumis.Common.Log;
using Eumis.Data.Notifications.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Notifications;
using Eumis.Domain.RequestPackages;
using Eumis.Job.Host.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Eumis.Job.Host.Jobs.InternalNotification
{
    public class NotificationDistributorJob : IJob
    {
        private TimeSpan period;
        private bool disposed;
        private ILogger logger;
        private IUnitOfWork unitOfWork;
        private IUserNotificationsRepository userNotificationsRepository;
        private INotificationEntriesRepository notificationEntriesRepository;
        private INotificationSettingsRepository notificationSettingsRepository;
        private INotificationEventsPermissionsRepository notificationEventsPermissionsRepository;
        private IRequestPackagesRepository requestPackagesRepository;
        private IUsersRepository usersRepository;

        public NotificationDistributorJob(
            ILogger logger,
            IUnitOfWork unitOfWork,
            IUserNotificationsRepository userNotificationsRepository,
            INotificationEntriesRepository notificationEntriesRepository,
            INotificationSettingsRepository userNotificationSettingsRepository,
            INotificationEventsPermissionsRepository notificationEventsPermissionsRepository,
            IRequestPackagesRepository requestPackagesRepository,
            IUsersRepository usersRepository)
        {
            this.period = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:NotificationDistributorJobPeriodInSeconds")));
            this.disposed = false;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.userNotificationsRepository = userNotificationsRepository;
            this.notificationEntriesRepository = notificationEntriesRepository;
            this.notificationSettingsRepository = userNotificationSettingsRepository;
            this.notificationEventsPermissionsRepository = notificationEventsPermissionsRepository;
            this.requestPackagesRepository = requestPackagesRepository;
            this.usersRepository = usersRepository;
        }

        public string Name => "NotificationDistributorJob";

        public TimeSpan Period => this.period;

        public void Action(CancellationToken token)
        {
            Dictionary<NotificationEntry, bool> pendingNotificationEntries = this.notificationEntriesRepository.FindPendingEntries();

            foreach (var notificationEntry in pendingNotificationEntries)
            {
                switch (notificationEntry.Key.NotificationEventId)
                {
                    case (int)NotificationEventType.RequestPackageStatusToEntered:
                        this.NotifyResponsibleUsersForRequestPackageEntered(notificationEntry);
                        break;
                    case (int)NotificationEventType.RequestPackageStatusToChecked:
                        this.NotifyResponsibleUsersForRequestPackageChecked(notificationEntry);
                        break;
                    case (int)NotificationEventType.RequestPackageStatusToDraft:
                        this.NotifyResponsibleUsersForRequestPackageEntered(notificationEntry);
                        break;
                    default:
                        this.NotifyResponsibleUsers(notificationEntry);
                        break;
                }

                notificationEntry.Key.ChangeStatus(NotificationEntryStatus.Done);
            }

            if (pendingNotificationEntries.Any())
            {
                this.unitOfWork.Save();
            }
        }

        private void NotifyResponsibleUsersForRequestPackageChecked(KeyValuePair<NotificationEntry, bool> notificationEntry)
        {
            var requestPackage = this.requestPackagesRepository.Find(notificationEntry.Key.DispatcherId);

            if (requestPackage.Type == RequestPackageType.Request)
            {
                var responsibleUsers = this.usersRepository.FindAllAdministrators().Where(u => u.IsActive && !u.IsLocked && !u.IsDeleted);
                var subscribedUsers = this.notificationSettingsRepository.GetSubscribedUsersForEntry(notificationEntry.Key, notificationEntry.Value);

                var subscribedResponsibleUsers = responsibleUsers.Select(u => u.UserId).Intersect(subscribedUsers).ToList();
                this.NotifySubscribedUsers(subscribedResponsibleUsers, notificationEntry.Key.NotificationEntryId);
            }
        }

        private void NotifyResponsibleUsersForRequestPackageEntered(KeyValuePair<NotificationEntry, bool> notificationEntry)
        {
            var requestPackage = this.requestPackagesRepository.Find(notificationEntry.Key.DispatcherId);

            if (requestPackage.Type == RequestPackageType.Request)
            {
                var responsibleUsers = this.usersRepository.FindAllControllingByOrganization(requestPackage.UserOrganizationId.Value).Where(u => u.IsActive && !u.IsLocked && !u.IsDeleted);
                var subscribedUsers = this.notificationSettingsRepository.GetSubscribedUsersForEntry(notificationEntry.Key, notificationEntry.Value);

                var subscribedResponsibleUsers = responsibleUsers.Select(u => u.UserId).Intersect(subscribedUsers).ToList();
                this.NotifySubscribedUsers(subscribedResponsibleUsers, notificationEntry.Key.NotificationEntryId);
            }
        }

        private void NotifyResponsibleUsers(KeyValuePair<NotificationEntry, bool> notificationEntry)
        {
            var subscribedUsers = this.notificationSettingsRepository.GetSubscribedUsersForEntry(notificationEntry.Key, notificationEntry.Value);

            var subscribedUsersWithPermission = this.notificationEventsPermissionsRepository.UsersHasNotificationEventPermission(subscribedUsers, notificationEntry.Key, notificationEntry.Value);

            this.NotifySubscribedUsers(subscribedUsersWithPermission, notificationEntry.Key.NotificationEntryId);
        }

        private void NotifySubscribedUsers(List<int> userIds, int notificationEntryId)
        {
            userIds.ForEach(
                x =>
                    this.userNotificationsRepository.Add(
                        new UserNotification(x, notificationEntryId)));
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                this.disposed = true;

                this.logger.Log(LogLevel.Info, "NotificationDistributor job disposed");
            }
        }
    }
}
