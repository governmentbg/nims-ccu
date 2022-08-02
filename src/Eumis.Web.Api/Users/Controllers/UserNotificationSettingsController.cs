using Eumis.ApplicationServices.Services.Notification;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Notifications.Repositories;
using Eumis.Data.Notifications.ViewObjects;
using Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Notifications.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Users.Controllers
{
    [RoutePrefix("api/users/{userId}/notificationSettings")]
    public class UserNotificationSettingsController : ApiController
    {
        private IUsersRepository usersRepository;
        private INotificationSettingsRepository notificationSettingsRepository;
        private IAuthorizer authorizer;

        public UserNotificationSettingsController(
            INotificationSettingsRepository userNotificationSettingsRepository,
            IUsersRepository usersRepository,
            IAuthorizer authorizer)
        {
            this.notificationSettingsRepository = userNotificationSettingsRepository;
            this.usersRepository = usersRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<NotificationSettingVO> GetUserNotificationSettings(int userId)
        {
            return this.notificationSettingsRepository.GetActiveUserNotificationSettings(userId);
        }

        [HttpGet]
        [Route("{notificationSettingId:int}")]
        public NotificationSettingDO GetUserNotificationSetting(int notificationSettingId)
        {
            var setting = this.notificationSettingsRepository.Find(notificationSettingId);

            return new NotificationSettingDO(setting);
        }

        [Route("{notificationSettingId:int}/attachedProgrammes")]
        public IList<EntityCodeNomVO> GetnotificationSettingAttachedProgrammes(int notificationSettingId)
        {
            return this.notificationSettingsRepository.GetAttachedProgrammes(notificationSettingId);
        }

        [Route("{notificationSettingId:int}/attachedProgrammePriorities")]
        public IList<ProgrammePriorityItemVO> GetNotificationSettingAttachedProgrammePriorities(int notificationSettingId)
        {
            return this.notificationSettingsRepository.GetAttachedProgrammePriorities(notificationSettingId);
        }

        [Route("{notificationSettingId:int}/attachedProcedures")]
        public IList<ProcedureVO> GetnotificationSettingAttachedProcedures(int notificationSettingId)
        {
            this.authorizer.AssertCanDo(ProcedureListActions.Search);

            return this.notificationSettingsRepository.GetAttachedProcedures(notificationSettingId);
        }

        [Route("{notificationSettingId:int}/attachedContracts")]
        public IList<ContractVO> GetnotificationSettingAttachedContracts(int notificationSettingId)
        {
            this.authorizer.AssertCanDo(ContractListActions.Search);

            return this.notificationSettingsRepository.GetAttachedContracts(notificationSettingId);
        }
    }
}
