using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users;
using System.Collections.Generic;

namespace Eumis.Data.Notifications.Repositories
{
    public interface INotificationEventsNomsRepository : IEntityCodeNomsRepository<NotificationEvent, EntityCodeNomVO>
    {
        IEnumerable<EntityCodeNomVO> GetNotificationEventsNoms(int[] eventIds, string term = null, int offset = 0, int? limit = null);

        NotificationEvent Find(int id);
    }
}
