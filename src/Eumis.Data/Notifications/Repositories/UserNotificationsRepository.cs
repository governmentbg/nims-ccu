using Eumis.Common.Db;
using Eumis.Data.Linq;
using Eumis.Data.Notifications.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Notifications;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.Procedures;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Notifications.Repositories
{
    internal class UserNotificationsRepository : AggregateRepository<UserNotification>, IUserNotificationsRepository
    {
        public UserNotificationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<UserNotificationVO> GetUserNotifications(int userId, int? notificationEventId = null, bool? isRead = null)
        {
            var predicate = PredicateBuilder.True<UserNotification>()
                .And(x => x.UserId == userId);

            if (isRead.HasValue)
            {
                predicate = predicate
                    .And(x => x.IsRead == isRead);
            }

            var eventPredicate = PredicateBuilder.True<NotificationEntry>();
            if (notificationEventId.HasValue)
            {
                eventPredicate = eventPredicate
                    .And(x => x.NotificationEventId == notificationEventId);
            }

            return (from un in this.unitOfWork.DbContext.Set<UserNotification>().Where(predicate)
                    join ne in this.unitOfWork.DbContext.Set<NotificationEntry>().Where(eventPredicate) on un.NotificationEntryId equals ne.NotificationEntryId
                    join ev in this.unitOfWork.DbContext.Set<NotificationEvent>() on ne.NotificationEventId equals ev.NotificationEventId

                    join op in this.unitOfWork.DbContext.Set<MapNode>() on ne.ProgrammeId equals op.MapNodeId into g1
                    from op in g1.DefaultIfEmpty()

                    join pp in this.unitOfWork.DbContext.Set<MapNode>() on ne.ProgrammePriorityId equals pp.MapNodeId into g2
                    from pp in g2.DefaultIfEmpty()

                    join p in this.unitOfWork.DbContext.Set<Procedure>() on ne.ProcedureId equals p.ProcedureId into g3
                    from p in g3.DefaultIfEmpty()

                    join c in this.unitOfWork.DbContext.Set<Contract>() on ne.ContractId equals c.ContractId into g4
                    from c in g4.DefaultIfEmpty()

                    select new UserNotificationVO
                    {
                        CreateDate = ne.CreateDate,
                        EventId = ev.NotificationEventId,
                        EventName = ev.Name,
                        IsRead = un.IsRead,
                        UserNotificationId = un.UserNotificationId,
                        DispatcherPath = ne.DispatcherPath,
                        DispatcherId = ne.DispatcherId,
                        ProgrammeCode = op.Code,
                        ProgrammePriorityCode = pp.Code,
                        ProcedureCode = p.Code,
                        ContractCode = c.RegNumber,
                    }).ToList();
        }

        public int GetNewNotificationCount(int userId)
        {
            return this.Set().Where(x => x.UserId == userId && x.IsRead == false).Count();
        }

        public UserNotificationVO GetUserNotification(int notificationId, int userId)
        {
            var notificationVO = this.GetUserNotifications(userId).Where(x => x.UserNotificationId == notificationId).Single();
            if (!notificationVO.IsRead)
            {
                var notification = this.Find(notificationId);
                notification.MarkAsRead();

                this.unitOfWork.Save();
            }

            return notificationVO;
        }
    }
}
