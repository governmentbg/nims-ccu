using System.Data.Entity;
using Eumis.Common.Db;
using Eumis.Domain.Notifications.NotificationSets;

namespace Eumis.Domain.Notifications
{
    public class NotificationsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NotificationSettingMap());
            modelBuilder.Configurations.Add(new NotificationSetMap());
            modelBuilder.Configurations.Add(new NotificationProgrammeSetMap());
            modelBuilder.Configurations.Add(new NotificationProgrammePrioritySetMap());
            modelBuilder.Configurations.Add(new NotificationProcedureSetMap());
            modelBuilder.Configurations.Add(new NotificationContractSetMap());
            modelBuilder.Configurations.Add(new NotificationEntryMap());
            modelBuilder.Configurations.Add(new UserNotificationMap());
            modelBuilder.Configurations.Add(new NotificationEventPermissionMap());
        }
    }
}
