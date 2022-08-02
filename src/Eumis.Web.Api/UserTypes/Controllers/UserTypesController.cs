using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.UserTypes.Repositories;
using Eumis.Data.UserTypes.ViewObjects;
using Eumis.Domain.UserTypes;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.UserTypes.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.UserTypes.Controllers
{
    [RoutePrefix("api/userTypes")]
    public class UserTypesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IUserTypesRepository userTypesRepository;
        private IAuthorizer authorizer;

        public UserTypesController(
            IUnitOfWork unitOfWork,
            IUserTypesRepository userTypesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.userTypesRepository = userTypesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<UserTypesVO> GetUserTypes()
        {
            this.authorizer.AssertCanDo(UserTypeListActions.Search);

            return this.userTypesRepository.GetUserTypes();
        }

        [Route("{userTypeId:int}")]
        public UserTypeDO GetUserType(int userTypeId)
        {
            this.authorizer.AssertCanDo(UserTypeActions.View, userTypeId);

            var userType = this.userTypesRepository.Find(userTypeId);

            return new UserTypeDO(userType);
        }

        [HttpGet]
        [Route("new")]
        public UserTypeDO NewUserType()
        {
            this.authorizer.AssertCanDo(UserTypeListActions.Create);

            return new UserTypeDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.UserTypes.Create))]
        public void CreateUserType(UserTypeDO userType)
        {
            this.authorizer.AssertCanDo(UserTypeListActions.Create);

            UserType newUserType = new UserType(
                userType.Name,
                userType.IsSuperUser,
                userType.PermissionTemplateId.Value,
                userType.UserOrganizationId.Value);

            this.userTypesRepository.Add(newUserType);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{userTypeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.UserTypes.Edit), IdParam = "userTypeId")]
        public void UpdateUserType(int userTypeId, UserTypeDO userType)
        {
            this.authorizer.AssertCanDo(UserTypeActions.Edit, userTypeId);

            UserType oldUserType = this.userTypesRepository.FindForUpdate(userTypeId, userType.Version);

            oldUserType.UpdateAttributes(
                userType.Name,
                userType.IsSuperUser);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{userTypeId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.UserTypes.Delete), IdParam = "userTypeId")]
        public void DeleteUserType(int userTypeId, string version)
        {
            this.authorizer.AssertCanDo(UserTypeActions.Delete, userTypeId);

            byte[] vers = System.Convert.FromBase64String(version);

            UserType oldUserType = this.userTypesRepository.FindForUpdate(userTypeId, vers);

            this.userTypesRepository.Remove(oldUserType);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{userTypeId:int}/canDelete")]
        public ErrorsDO CanDeleteUserType(int userTypeId)
        {
            this.authorizer.AssertCanDo(UserTypeActions.Edit, userTypeId);

            var errors = this.userTypesRepository.CanDeleteUserType(userTypeId);

            return new ErrorsDO(errors);
        }
    }
}
