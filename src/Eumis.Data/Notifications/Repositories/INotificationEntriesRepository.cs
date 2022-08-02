using Eumis.Domain.NonAggregates;
using Eumis.Domain.Notifications;
using System.Collections.Generic;

namespace Eumis.Data.Notifications.Repositories
{
    public interface INotificationEntriesRepository : IAggregateRepository<NotificationEntry>
    {
        IList<NotificationEvent> GetNotificationEvents();

        NotificationEvent GetNotificationEvent(NotificationEventType eventType);

        Dictionary<NotificationEntry, bool> FindPendingEntries();
    }
}
