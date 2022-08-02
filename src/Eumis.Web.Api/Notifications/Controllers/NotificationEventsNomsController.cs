using Eumis.Common.Auth;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Notifications.Repositories;
using Eumis.Data.Users.Repositories;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

namespace Eumis.Web.Api.Notifications.Controllers
{
    [RoutePrefix("api/nomenclatures/notificationEvents")]
    public class NotificationEventsNomsController : ApiController
    {
        private INotificationEventsNomsRepository notificationEventsNomsRepository;
        private IAccessContext accessContext;
        private INotificationEventsPermissionsRepository notificationEventsPermissionsRepository;
        private IUsersRepository usersRepository;

        public NotificationEventsNomsController(
            INotificationEventsNomsRepository notificationEventsNomsRepository,
            IAccessContext accessContext,
            INotificationEventsPermissionsRepository notificationEventsPermissionsRepository,
            IUsersRepository usersRepository)
        {
            this.notificationEventsNomsRepository = notificationEventsNomsRepository;
            this.accessContext = accessContext;
            this.usersRepository = usersRepository;
            this.notificationEventsPermissionsRepository = notificationEventsPermissionsRepository;
        }

        [Route("{id:int}")]
        public EntityCodeNomVO GetNom(int id)
        {
            return this.notificationEventsNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityCodeNomVO> GetNoms(string term = null, int offset = 0, int? limit = null)
        {
            var availableNotificationIds = this.notificationEventsPermissionsRepository.GetAvailableNotificationEventIds(this.accessContext.UserId);
            return this.notificationEventsNomsRepository.GetNotificationEventsNoms(availableNotificationIds, term, offset, limit);
        }
    }
}
