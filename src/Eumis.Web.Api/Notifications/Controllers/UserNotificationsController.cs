using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Notifications.Repositories;
using Eumis.Data.Notifications.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.NonAggregates;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Web.Api.Notifications.Controllers
{
    [RoutePrefix("api/notifications")]
    public class UserNotificationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IUserNotificationsRepository userNotificationsRepository;
        private IAuthorizer authorizer;
        private UserClaimsContextFactory userClaimsContextFactory;
        private IAccessContext accessContext;
        private IUserClaimsContext currentUserClaimsContext;

        public UserNotificationsController(
            IUnitOfWork unitOfWork,
            IUserNotificationsRepository userNotificationsRepository,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            UserClaimsContextFactory userClaimsContextFactory)
        {
            this.unitOfWork = unitOfWork;
            this.userNotificationsRepository = userNotificationsRepository;
            this.authorizer = authorizer;
            this.userClaimsContextFactory = userClaimsContextFactory;
            this.accessContext = accessContext;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("")]
        public List<UserNotificationVO> GetNotifications(int? notificationEventId = null, bool? isRead = null)
        {
            var userId = this.accessContext.UserId;
            return this.userNotificationsRepository.GetUserNotifications(userId, notificationEventId, isRead);
        }

        [Route("{notificationId:int}")]
        public UserNotificationVO GetNotification(int notificationId)
        {
            int userId = this.accessContext.UserId;
            var userNotification = this.userNotificationsRepository.GetUserNotification(notificationId, userId);

            return userNotification;
        }

        [HttpDelete]
        [Route("{notificationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.UserNotifications.Delete), IdParam = "notificationId")]
        public void DeleteNotification(int notificationId)
        {
            var userId = this.accessContext.UserId;
            var notification = this.userNotificationsRepository.Find(notificationId);
            if (notification.UserId != userId)
            {
                throw new DomainException("Current user cannot delete specified notification");
            }

            this.userNotificationsRepository.Remove(notification);
            this.unitOfWork.Save();
        }

        [Route("newNotifications")]
        public int GetNewMessagesCount()
        {
            var userId = this.accessContext.UserId;
            return this.userNotificationsRepository.GetNewNotificationCount(userId);
        }
    }
}
