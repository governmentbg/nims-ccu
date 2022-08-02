using Eumis.ApplicationServices.Services.Notification;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.Notifications.Repositories;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Notifications.Controllers
{
    [RoutePrefix("api/notificationSettings/{notificationSettingId}/attachedProgrammePriorities")]
    public class NotificationSettingAttachedProgrammePrioritiesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private INotificationSettingsRepository notificationSettingsRepository;
        private INotificationSettingService notificationSettingService;
        private IAccessContext accessContext;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public NotificationSettingAttachedProgrammePrioritiesController(
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

        [Route("pPriorities")]
        public IList<ProgrammePriorityItemVO> GetProgrammePrioritiesForNotificationSettingAttachedProgrammePriorities(int notificationSettingId)
        {
            return this.notificationSettingService.GetUnattachedProgrammePriorities(notificationSettingId);
        }

        [Route("")]
        public IList<ProgrammePriorityItemVO> GetNotificationSettingAttachedProgrammePriorities(int notificationSettingId)
        {
            return this.notificationSettingService.GetAttachedProgrammePriorities(notificationSettingId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.NotificationSettings.Edit.AttachedProgrammePriorities.Edit), IdParam = "notificationSettingId")]
        public void AddNotificationSettingAttachedProgrammePriority(int notificationSettingId, string version, int[] attachedProgrammePriorityIds)
        {
            byte[] vers = System.Convert.FromBase64String(version);

            var userId = this.accessContext.UserId;

            this.notificationSettingService.AddSelectedProgrammePriorities(notificationSettingId, vers, attachedProgrammePriorityIds, userId);
        }

        [HttpDelete]
        [Route("{attachedProgrammePriorityId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.NotificationSettings.Edit.AttachedProgrammePriorities.Delete), IdParam = "notificationSettingId", ChildIdParam = "attachedProgrammePriorityId")]
        public void DeleteNotificationSettingAttachedProgrammePriority(int notificationSettingId, int attachedProgrammePriorityId, string version)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            var userId = this.accessContext.UserId;

            this.notificationSettingService.RemoveSelectedProgrammePriority(notificationSettingId, vers, attachedProgrammePriorityId, userId);
        }
    }
}
