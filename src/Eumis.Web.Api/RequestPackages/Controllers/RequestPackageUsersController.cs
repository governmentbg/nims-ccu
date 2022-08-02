using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.RequestPackages.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.RequestPackages;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.RequestPackages.Controllers
{
    [RoutePrefix("api/requestPackages/{requestPackageId}/users")]
    public class RequestPackageUsersController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRequestPackagesRepository requestPackagesRepository;
        private IUsersRepository usersRepository;
        private IAuthorizer authorizer;

        public RequestPackageUsersController(
            IUnitOfWork unitOfWork,
            IRequestPackagesRepository requestPackagesRepository,
            IUsersRepository usersRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.requestPackagesRepository = requestPackagesRepository;
            this.usersRepository = usersRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<RequestPackageUserVO> GetRequestPackageUsers(int requestPackageId)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.View, requestPackageId);

            return this.requestPackagesRepository.GetRequestPackageUsers(requestPackageId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.Users.Create), IdParam = "requestPackageId")]
        public void AddRequestPackageUsers(int requestPackageId, string version, int[] userIds)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);

            byte[] vers = System.Convert.FromBase64String(version);
            RequestPackage requestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, vers);

            var users = this.usersRepository.FindAll(userIds);

            foreach (var user in users)
            {
                requestPackage.AddRequestPackageUser(user.UserId, user.UserOrganizationId);
            }

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{userId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.Users.Delete), IdParam = "requestPackageId")]
        public void DeleteRequestPackageUser(int requestPackageId, int userId, string version)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);

            byte[] vers = System.Convert.FromBase64String(version);
            RequestPackage requestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, vers);

            requestPackage.RemoveRequestPackageUser(userId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("changeStatusToChecked")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.Users.ChangeStatusToChecked), IdParam = "requestPackageId", ChildIdParam = "userId")]
        public void ChangeStatusToChecked(int requestPackageId, int userId, string version)
        {
            this.ChangeStatus(requestPackageId, userId, version, RequestStatus.Checked);
        }

        [HttpPut]
        [Route("changeStatusToActive")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.Users.ChangeStatusToActive), IdParam = "requestPackageId", ChildIdParam = "userId")]
        public void ChangeStatusToActive(int requestPackageId, int userId, string version)
        {
            this.ChangeStatus(requestPackageId, userId, version, RequestStatus.Active);
        }

        [HttpPut]
        [Route("changeStatusToRejected")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.Users.ChangeStatusToRejected), IdParam = "requestPackageId", ChildIdParam = "userId")]
        public void ChangeStatusToRejected(int requestPackageId, int userId, string version, ConfirmDO confirm)
        {
            this.ChangeStatus(requestPackageId, userId, version, RequestStatus.Rejected, confirm.Note);
        }

        private void ChangeStatus(int requestPackageId, int userId, string version, RequestStatus requestStatus, string rejectionMessage = "")
        {
            this.authorizer.AssertCanDo(RequestPackageActions.ChangeUserStatus, requestPackageId);

            byte[] vers = System.Convert.FromBase64String(version);
            RequestPackage oldRequestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, vers);

            oldRequestPackage.ChangeRequestPackageUserStatus(userId, requestStatus, rejectionMessage);

            this.unitOfWork.Save();
        }
    }
}
