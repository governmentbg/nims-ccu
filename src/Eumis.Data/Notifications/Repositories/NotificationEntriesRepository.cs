using Eumis.Common.Db;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Notifications.Repositories
{
    internal class NotificationEntriesRepository : AggregateRepository<NotificationEntry>, INotificationEntriesRepository
    {
        public NotificationEntriesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<NotificationEntry, object>>[] Includes
        {
            get
            {
                return new Expression<Func<NotificationEntry, object>>[]
                {
                    c => c.NotificationEvent,
                };
            }
        }

        public IList<NotificationEvent> GetNotificationEvents()
        {
            return this.unitOfWork.DbContext.Set<NotificationEvent>().ToList();
        }

        public NotificationEvent GetNotificationEvent(NotificationEventType eventType)
        {
            return this.unitOfWork.DbContext.Set<NotificationEvent>().Where(x => x.NotificationEventId == (int)eventType).Single();
        }

        public Dictionary<NotificationEntry, bool> FindPendingEntries()
        {
            return (from ne in this.unitOfWork.DbContext.Set<NotificationEntry>()
                    join e in this.unitOfWork.DbContext.Set<NotificationEvent>() on ne.NotificationEventId equals e.NotificationEventId
                    where ne.Status == NotificationEntryStatus.Pending || ne.Status == NotificationEntryStatus.UnknownError
                    select new { ne, e.IsProgrammeDependent }).ToDictionary(x => x.ne, x => x.IsProgrammeDependent);
        }
    }
}
