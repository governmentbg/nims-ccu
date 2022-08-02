using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Notifications;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.Notifications.Controllers
{
    [RoutePrefix("api/nomenclatures/notificationScopes")]
    public class NotificationScopesNomsController : EnumNomsController<NotificationScope>
    {
        public NotificationScopesNomsController(IEnumNomsRepository<NotificationScope> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
