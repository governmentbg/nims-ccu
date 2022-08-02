using Eumis.ApplicationServices.Services.Notification;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Relations;
using Eumis.Data.Notifications.Repositories;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Notifications.Controllers
{
    [RoutePrefix("api/notificationSettings/{notificationSettingId}/attachedProgrammes")]
    public class NotificationSettingAttachedProgrammesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private INotificationSettingsRepository notificationSettingsRepository;
        private INotificationSettingService notificationSettingService;
        private IAccessContext accessContext;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public NotificationSettingAttachedProgrammesController(
            IUnitOfWork unitOfWork,
            INotificationSettingsRepository notificationSettingsRepository,
            INotificationSettingService notificationSettingService,
            IAuthorizer authorizer,
            IRelationsRepository relationsRepository,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.notificationSettingsRepository = notificationSettingsRepository;
            this.notificationSettingService = notificationSettingService;
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
            this.accessContext = accessContext;
        }

        [Route("Programmes")]
        public IList<EntityCodeNomVO> GetProgrammesForNotificationSettingAttachedProgrammes(int notificationSettingId)
        {
            var userId = this.accessContext.UserId;
            return this.notificationSettingService.GetUnattachedProgrammes(notificationSettingId, userId);
        }

        [Route("")]
        public IList<EntityCodeNomVO> GetnotificationSettingAttachedProgrammes(int notificationSettingId)
        {
            return this.notificationSettingService.GetAttachedProgrammes(notificationSettingId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.NotificationSettings.Edit.AttachedProgrammes.Edit), IdParam = "notificationSettingId")]
        public void AddNotificationSettingAttachedProgramme(int notificationSettingId, string version, int[] attachedProgrammeIds)
        {
            byte[] vers = System.Convert.FromBase64String(version);

            var userId = this.accessContext.UserId;

            this.notificationSettingService.AddSelectedProgrammes(notificationSettingId, vers, attachedProgrammeIds, userId);
        }

        [HttpDelete]
        [Route("{attachedProgrammeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.NotificationSettings.Edit.AttachedProgrammes.Delete), IdParam = "notificationSettingId", ChildIdParam = "attachedProgrammeId")]
        public void DeleteNotificationSettingAttachedProgramme(int notificationSettingId, int attachedProgrammeId, string version)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            var userId = this.accessContext.UserId;

            this.notificationSettingService.RemoveSelectedProgramme(notificationSettingId, vers, attachedProgrammeId, userId);
        }
    }
}
