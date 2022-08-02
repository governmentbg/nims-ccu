using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Users.Repositories;
using Eumis.Data.UserTypes.Repositories;
using Eumis.Domain.RequestPackages;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.RequestPackages.DataObjects;
using System;
using System.Web.Http;

namespace Eumis.Web.Api.RequestPackages.Controllers
{
    [RoutePrefix("api/requestPackages/{requestPackageId}/users/{userId}/regDataRequests")]
    public class RequestPackageUserRegDataRequestsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRequestPackagesRepository requestPackagesRepository;
        private IUsersRepository usersRepository;
        private IUserTypesRepository userTypesRepository;
        private IUserClaimsContext currentUserClaimsContext;
        private IAuthorizer authorizer;

        public RequestPackageUserRegDataRequestsController(
            IUnitOfWork unitOfWork,
            IRequestPackagesRepository requestPackagesRepository,
            IUsersRepository usersRepository,
            IUserTypesRepository userTypesRepository,
            UserClaimsContextFactory userClaimsContextFactory,
            IAuthorizer authorizer,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.requestPackagesRepository = requestPackagesRepository;
            this.usersRepository = usersRepository;
            this.userTypesRepository = userTypesRepository;
            this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            this.authorizer = authorizer;
        }

        [Route("")]
        public RegDataRequestDO GetRequestPackageUserRegDataRequest(int requestPackageId, int userId)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.View, requestPackageId);

            var requestPackage = this.requestPackagesRepository.Find(requestPackageId);

            var regDataRequest = requestPackage.FindRequestPackageUserRegDataRequest(userId);

            return new RegDataRequestDO(regDataRequest);
        }

        [Route("new")]
        public RegDataRequestDO GetNewRequestPackageUserRegDataRequest(int requestPackageId, int userId)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);

            var user = this.usersRepository.Find(userId);

            return new RegDataRequestDO(requestPackageId, user);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.UserRegDataRequests.Create), IdParam = "requestPackageId")]
        public void AddRequestPackageUserRegDataRequest(int requestPackageId, int userId, string version, RegDataRequestDO regDataRequest)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);
            byte[] vers = System.Convert.FromBase64String(version);
            var requestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, vers);

            if (this.currentUserClaimsContext.IsSuperUser)
            {
                requestPackage.AddRequestPackageUserRegDataRequest(
                    userId,
                    regDataRequest.Uin,
                    regDataRequest.Fullname,
                    regDataRequest.Email,
                    regDataRequest.Phone,
                    regDataRequest.Address,
                    regDataRequest.Position,
                    regDataRequest.UserOrganizationId,
                    regDataRequest.UserTypeId);
            }
            else
            {
                int userTypeId = this.usersRepository.GetUserTypeId(userId);

                requestPackage.AddRequestPackageUserRegDataRequest(
                    userId,
                    regDataRequest.Uin,
                    regDataRequest.Fullname,
                    regDataRequest.Email,
                    regDataRequest.Phone,
                    regDataRequest.Address,
                    regDataRequest.Position,
                    this.currentUserClaimsContext.UserOrganizationId,
                    userTypeId);
            }

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("update")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.UserRegDataRequests.Edit), IdParam = "requestPackageId", ChildIdParam = "userId")]
        public void UpdateRequestPackageUserRegDataRequest(int requestPackageId, int userId, string version, RegDataRequestDO regDataRequest)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);

            byte[] vers = System.Convert.FromBase64String(version);
            var requestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, vers);

            if (this.currentUserClaimsContext.IsSuperUser)
            {
                requestPackage.UpdateRequestPackageUserRegDataRequest(
                    userId,
                    regDataRequest.Uin,
                    regDataRequest.Fullname,
                    regDataRequest.Email,
                    regDataRequest.Phone,
                    regDataRequest.Address,
                    regDataRequest.Position,
                    regDataRequest.UserOrganizationId,
                    regDataRequest.UserTypeId);
            }
            else
            {
                int userTypeId = this.usersRepository.GetUserTypeId(userId);

                requestPackage.UpdateRequestPackageUserRegDataRequest(
                    userId,
                    regDataRequest.Uin,
                    regDataRequest.Fullname,
                    regDataRequest.Email,
                    regDataRequest.Phone,
                    regDataRequest.Address,
                    regDataRequest.Position,
                    this.currentUserClaimsContext.UserOrganizationId,
                    userTypeId);
            }

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.UserRegDataRequests.Delete), IdParam = "requestPackageId", ChildIdParam = "userId")]
        public void DeleteRequestPackageUserRegDataRequest(int requestPackageId, int userId, string version)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);

            byte[] vers = System.Convert.FromBase64String(version);
            RequestPackage requestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, vers);

            requestPackage.RemoveRequestPackageUserRegDataRequest(userId);

            this.unitOfWork.Save();
        }
    }
}
