using Eumis.ApplicationServices.Services.Notification;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Relations;
using Eumis.Data.Notifications.Repositories;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Notifications.Controllers
{
    [RoutePrefix("api/notificationSettings/{notificationSettingId}/attachedContracts")]
    public class NotificationSettingAttachedContractsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private INotificationSettingsRepository notificationSettingsRepository;
        private INotificationSettingService notificationSettingService;
        private IAccessContext accessContext;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public NotificationSettingAttachedContractsController(
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

        [Route("contracts")]
        public IList<ContractVO> GetContractsForNotificationSettingAttachedContracts(int notificationSettingId)
        {
            this.authorizer.AssertCanDo(ContractListActions.Search);

            return this.notificationSettingService.GetUnattachedContracts(notificationSettingId);
        }

        [Route("")]
        public IList<ContractVO> GetnotificationSettingAttachedContracts(int notificationSettingId)
        {
            this.authorizer.AssertCanDo(ContractListActions.Search);

            return this.notificationSettingService.GetAttachedContracts(notificationSettingId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.NotificationSettings.Edit.AttachedContracts.Edit), IdParam = "notificationSettingId")]
        public void AddNotificationSettingAttachedContract(int notificationSettingId, string version, int[] attachedContractIds)
        {
            byte[] vers = System.Convert.FromBase64String(version);

            var userId = this.accessContext.UserId;

            this.notificationSettingService.AddSelectedContracts(notificationSettingId, vers, attachedContractIds, userId);
        }

        [HttpDelete]
        [Route("{attachedContractId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.NotificationSettings.Edit.AttachedContracts.Delete), IdParam = "notificationSettingId", ChildIdParam = "attachedContractId")]
        public void DeleteNotificationSettingAttachedContract(int notificationSettingId, int attachedContractId, string version)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            var userId = this.accessContext.UserId;

            this.notificationSettingService.RemoveSelectedContract(notificationSettingId, vers, attachedContractId, userId);
        }
    }
}
