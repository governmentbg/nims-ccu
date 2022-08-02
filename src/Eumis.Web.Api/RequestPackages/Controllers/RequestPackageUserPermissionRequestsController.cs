using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.PermissionTemplates.Repositories;
using Eumis.Data.PermissionTemplates.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.RequestPackages;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.RequestPackages.DataObjects;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.RequestPackages.Controllers
{
    [RoutePrefix("api/requestPackages/{requestPackageId}/users/{userId}/permissionRequests")]
    public class RequestPackageUserPermissionRequestsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRequestPackagesRepository requestPackagesRepository;
        private IUsersRepository usersRepository;
        private IProgrammesRepository programmesRepository;
        private IPermissionTemplatesRepository permissionTemplatesRepository;
        private IProgrammeCacheManager programmeCacheManager;
        private IAuthorizer authorizer;

        public RequestPackageUserPermissionRequestsController(
            IUnitOfWork unitOfWork,
            IRequestPackagesRepository requestPackagesRepository,
            IUsersRepository usersRepository,
            IProgrammesRepository programmesRepository,
            IPermissionTemplatesRepository permissionTemplatesRepository,
            IProgrammeCacheManager programmeCacheManager,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.requestPackagesRepository = requestPackagesRepository;
            this.usersRepository = usersRepository;
            this.programmesRepository = programmesRepository;
            this.permissionTemplatesRepository = permissionTemplatesRepository;
            this.programmeCacheManager = programmeCacheManager;
            this.authorizer = authorizer;
        }

        [Route("")]
        public PermissionRequestDO GetRequestPackageUserPermissionRequest(int requestPackageId, int userId)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.View, requestPackageId);

            var requestPackage = this.requestPackagesRepository.Find(requestPackageId);
            var permissionRequest = requestPackage.FindRequestPackageUserPermissionRequest(userId);

            var programmes = this.programmesRepository.GetProgrammesIdAndShortName();

            return new PermissionRequestDO(permissionRequest, programmes);
        }

        [Route("new")]
        public PermissionRequestDO GetNewRequestPackageUserPermissionRequest(int requestPackageId, int userId)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);

            var user = this.usersRepository.Find(userId);
            int userTypeId = user.UserTypeId;

            var requestPackage = this.requestPackagesRepository.Find(requestPackageId);
            var permissionRequest = requestPackage.FindRequestPackageUser(userId);
            if (permissionRequest.RegDataRequest != null)
            {
                userTypeId = permissionRequest.RegDataRequest.UserTypeId;
            }

            var permissionTemplate = this.permissionTemplatesRepository.FindByUserType(userTypeId);
            var programmes = this.programmesRepository.GetProgrammesIdAndShortName();

            return new PermissionRequestDO(requestPackageId, userId, programmes, permissionTemplate, user.GetUserPermissions(programmes.Keys.ToArray()));
        }

        [Route("userInfo")]
        public PermissionTemplateUserInfoVO GetUserInfoRequestPackageUserPermissionRequest(int requestPackageId, int userId)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);

            var user = this.usersRepository.Find(userId);
            int userTypeId = user.UserTypeId;

            var requestPackage = this.requestPackagesRepository.Find(requestPackageId);
            var permissionRequest = requestPackage.FindRequestPackageUser(userId);
            if (permissionRequest.RegDataRequest != null)
            {
                userTypeId = permissionRequest.RegDataRequest.UserTypeId;
            }

            return this.permissionTemplatesRepository.GetUserInfo(userTypeId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.UserPermissionRequests.Create), IdParam = "requestPackageId", DisablePostData = true)]
        public void AddRequestPackageUserPermissionRequest(int requestPackageId, int userId, string version, PermissionRequestDO permissionRequest)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);

            byte[] vers = System.Convert.FromBase64String(version);
            var requestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, vers);
            var programmeIds = this.programmeCacheManager.ProgrammeIds;

            requestPackage.AddRequestPackageUserPermissionRequest(
                userId,
                permissionRequest.Permissions.GetPermissions(programmeIds),
                permissionRequest.PermissionTemplate.UserPermissions.GetPermissions(programmeIds));

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("update")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.UserPermissionRequests.Edit), IdParam = "requestPackageId", ChildIdParam = "userId")]
        public void UpdateRequestPackageUserPermissionRequest(int requestPackageId, int userId, string version, PermissionRequestDO permissionRequest)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);

            byte[] vers = System.Convert.FromBase64String(version);
            var requestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, vers);
            var programmeIds = this.programmeCacheManager.ProgrammeIds;

            requestPackage.UpdateRequestPackageUserPermissionRequest(
                userId,
                permissionRequest.Permissions.GetPermissions(programmeIds));

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.RequestPackages.Edit.UserPermissionRequests.Delete), IdParam = "requestPackageId", ChildIdParam = "userId")]
        public void DeleteRequestPackageUserPermissionRequest(int requestPackageId, int userId, string version)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.Edit, requestPackageId);

            byte[] vers = System.Convert.FromBase64String(version);
            RequestPackage requestPackage = this.requestPackagesRepository.FindForUpdate(requestPackageId, vers);

            requestPackage.RemoveRequestPackageUserPermissionRequest(userId);

            this.unitOfWork.Save();
        }
    }
}
