using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Notifications.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Notifications;
using Eumis.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Notifications.Repositories
{
    internal class NotificationEventsNomsRepository : Repository, INotificationEventsNomsRepository
    {
        public NotificationEventsNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityCodeNomVO GetNom(int nomValueId)
        {
            return (from ne in this.unitOfWork.DbContext.Set<NotificationEvent>()
                    where ne.NotificationEventId == nomValueId
                    select new EntityCodeNomVO
                    {
                        NomValueId = ne.NotificationEventId,
                        Name = ne.Name,
                        Code = ne.IsProgrammeDependent.ToString(),
                    })
                .SingleOrDefault();
        }

        public int GetNomIdByCode(string code)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityCodeNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return this.GetNoms(term, offset, limit);
        }

        public IEnumerable<EntityCodeNomVO> GetNotificationEventsNoms(int[] eventIds, string term = null, int offset = 0, int? limit = null)
        {
            return this.unitOfWork.DbContext.Set<NotificationEvent>()
                .Where(z => eventIds.Contains(z.NotificationEventId))
                .Select(x => new EntityCodeNomVO() { Code = x.IsProgrammeDependent.ToString(), Name = x.Name, NomValueId = x.NotificationEventId });
        }

        public NotificationEvent Find(int id)
        {
            return this.unitOfWork.DbContext.Set<NotificationEvent>().Where(x => x.NotificationEventId == id).Single();
        }
    }
}
