using Eumis.ApplicationServices.Services.Notification;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Data.Notifications.Repositories;
using Eumis.Data.Notifications.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Notifications;
using Eumis.Domain.Notifications.DataObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Notifications.Controllers
{
    [RoutePrefix("api/notificationSettings")]
    public class NotificationSettingsController : ApiController
    {
        private INotificationSettingsRepository notificationSettingsRepository;
        private IAccessContext accessContext;
        private INotificationSettingService notificationSettingService;

        public NotificationSettingsController(
            INotificationSettingsRepository userNotificationSettingsRepository,
            IAccessContext accessContext,
            INotificationSettingService notificationSettingService)
        {
            this.notificationSettingsRepository = userNotificationSettingsRepository;
            this.accessContext = accessContext;
            this.notificationSettingService = notificationSettingService;
        }

        [Route("")]
        public IList<NotificationSettingVO> GetNotificationSettings()
        {
            var userId = this.accessContext.UserId;
            return this.notificationSettingsRepository.GetUserNotificationSettings(userId);
        }

        [HttpGet]
        [Route("new")]
        public NotificationSettingDO CreateNotificationSetting()
        {
            var createDate = DateTime.Now;
            return new NotificationSettingDO()
            {
                Status = NotificationSettingStatus.Draft,
                CreateDate = createDate,
                ModifyDate = createDate,
            };
        }

        [HttpGet]
        [Route("copyUserSettings/{userId:int}")]
        public void CopyUserSettings(int userId)
        {
            var currentUserId = this.accessContext.UserId;
            if (userId == currentUserId)
            {
                throw new DomainException("User cannot copy it's own settings");
            }

            this.notificationSettingsRepository.CopyUserSettings(userId, currentUserId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.NotificationSettings.Create))]
        public object CreateNotificationSetting(NotificationSettingDO notificationSetting)
        {
            var userId = this.accessContext.UserId;
            var newNotificationSetting = this.notificationSettingService.CreateNotificationSetting(notificationSetting, userId);

            return new { notificationSettingId = newNotificationSetting.NotificationSettingId };
        }

        [HttpDelete]
        [Route("{notificationSettingId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.NotificationSettings.Delete), IdParam = "notificationSettingId")]
        public void DeleteNotificationSettingAttachedContract(int notificationSettingId, string version)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            var userId = this.accessContext.UserId;

            this.notificationSettingService.DeleteSetting(notificationSettingId, vers, userId);
        }

        [HttpGet]
        [Route("{notificationSettingId:int}/info")]
        public NotificationSettingInfoVO GetInfo(int notificationSettingId)
        {
            return this.notificationSettingsRepository.GetInfo(notificationSettingId);
        }

        [HttpGet]
        [Route("{notificationSettingId:int}")]
        public NotificationSettingDO GetNotificationSetting(int notificationSettingId)
        {
            var setting = this.notificationSettingsRepository.Find(notificationSettingId);
            if (setting.UserId != this.accessContext.UserId)
            {
                throw new UnauthorizedAccessException("Current user can't read specified setting");
            }

            return new NotificationSettingDO(setting);
        }

        [HttpPut]
        [Route("{notificationSettingId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.NotificationSettings.Edit.BasicData.Edit), IdParam = "notificationSettingId")]
        public void UpdateNotificationSetting(NotificationSettingDO notificationSetting)
        {
            this.notificationSettingService.UpdateNotificationSetting(notificationSetting, this.accessContext.UserId);
        }

        [HttpGet]
        [Route("{notificationSettingId:int}/canChangeStatusToActual")]
        public ErrorsDO CanChangeStatusToActual()
        {
            return new ErrorsDO();
        }

        [HttpGet]
        [Route("{notificationSettingId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeStatusToDraft()
        {
            return new ErrorsDO();
        }

        [HttpGet]
        [Route("{notificationSettingId:int}/changeStatusToActual")]
        public void ChangeStatusToActual(int notificationSettingId, string version)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            this.notificationSettingService.ChangeStatus(notificationSettingId, NotificationSettingStatus.Actual, vers, this.accessContext.UserId);
        }

        [HttpGet]
        [Route("{notificationSettingId:int}/changeStatusToDraft")]
        public void ChangeStatusToDraft(int notificationSettingId, string version)
        {
            byte[] vers = System.Convert.FromBase64String(version);
            this.notificationSettingService.ChangeStatus(notificationSettingId, NotificationSettingStatus.Draft, vers, this.accessContext.UserId);
        }
    }
}
