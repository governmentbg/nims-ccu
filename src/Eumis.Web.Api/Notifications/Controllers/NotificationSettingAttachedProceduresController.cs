using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.Notification;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Web.Api.Notifications.Controllers
{
    [RoutePrefix("api/notificationSettings/{notificationSettingId}/attachedProcedures")]
    public class NotificationSettingAttachedProceduresController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private INotificationSettingService notificationSettingService;
        private IAccessContext accessContext;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public NotificationSettingAttachedProceduresController(
            IUnitOfWork unitOfWork,
            INotificationSettingService notificationSettingService,
            IAuthorizer authorizer,
            IRelationsRepository relationsRepository,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.notificationSettingService = notificationSettingService;
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
            this.accessContext = accessContext;
        }

        [Route("procedures")]
        public IList<ProcedureVO> GetProceduresForNotificationSettingAttachedProcedures(int notificationSettingId)
        {
            this.authorizer.AssertCanDo(ProcedureListActions.Search);

            return this.notificationSettingService.GetUnattachedProcedures(notificationSettingId);
        }

        [Route("")]
        public IList<ProcedureVO> GetnotificationSettingAttachedProcedures(int notificationSettingId)
        {
            this.authorizer.AssertCanDo(ProcedureListActions.Search);

            return this.notificationSettingService.GetAttachedProcedures(notificationSettingId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.NotificationSettings.Edit.AttachedProcedures.Edit), IdParam = "notificationSettingId")]
        public void AddNotificationSettingAttachedProcedure(int notificationSettingId, string version, int[] attachedProcedureIds)
        {
            byte[] vers = System.Convert.FromBase64String(version);

            var userId = this.accessContext.UserId;

            this.notificationSettingService.AddSelectedProcedures(notificationSettingId, vers, attachedProcedureIds, userId);
        }

        [HttpDelete]
        [Route("{attachedProcedureId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.NotificationSettings.Edit.AttachedProcedures.Delete), IdParam = "notificationSettingId", ChildIdParam = "attachedProcedureId")]
        public void DeleteNotificationSettingAttachedProcedure(int notificationSettingId, int attachedProcedureId, string version)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            var userId = this.accessContext.UserId;

            this.notificationSettingService.RemoveSelectedProcedure(notificationSettingId, vers, attachedProcedureId, userId);
        }
    }
}
