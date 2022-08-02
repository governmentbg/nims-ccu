using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.UserOrganizations.Repositories;
using Eumis.Data.UserOrganizations.ViewObjects;
using Eumis.Domain.UserOrganizations;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.UserOrganizations.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.UserOrganizations.Controllers
{
    [RoutePrefix("api/userOrganizations")]
    public class UserOrganizationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IUserOrganizationsRepository userOrganizationsRepository;
        private IAuthorizer authorizer;

        public UserOrganizationsController(
            IUnitOfWork unitOfWork,
            IUserOrganizationsRepository userOrganizationsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.userOrganizationsRepository = userOrganizationsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<UserOrganizationsVO> GetUserOrganizations()
        {
            this.authorizer.AssertCanDo(UserOrganizationListActions.Search);

            return this.userOrganizationsRepository.GetUserOrganizations();
        }

        [Route("{userOrganizationId:int}")]
        public UserOrganizationDO GetUserOrganization(int userOrganizationId)
        {
            this.authorizer.AssertCanDo(UserOrganizationActions.View, userOrganizationId);

            var userOrganization = this.userOrganizationsRepository.Find(userOrganizationId);

            return new UserOrganizationDO(userOrganization);
        }

        [HttpGet]
        [Route("new")]
        public UserOrganizationDO NewUserOrganization()
        {
            this.authorizer.AssertCanDo(UserOrganizationListActions.Create);

            return new UserOrganizationDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.UserOrganizations.Create))]
        public void CreateUserOrganization(UserOrganizationDO userOrganization)
        {
            this.authorizer.AssertCanDo(UserOrganizationListActions.Create);

            UserOrganization newUserOrganization = new UserOrganization(userOrganization.Name);

            this.userOrganizationsRepository.Add(newUserOrganization);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{userOrganizationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.UserOrganizations.Edit), IdParam = "userOrganizationId")]
        public void UpdateUserOrganization(int userOrganizationId, UserOrganizationDO userOrganization)
        {
            this.authorizer.AssertCanDo(UserOrganizationActions.Edit, userOrganizationId);

            UserOrganization oldUserOrganization = this.userOrganizationsRepository.FindForUpdate(userOrganizationId, userOrganization.Version);

            oldUserOrganization.UpdateUserOrganization(
                userOrganization.Name);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{userOrganizationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.UserOrganizations.Delete), IdParam = "userOrganizationId")]
        public void DeleteUserOrganization(int userOrganizationId, string version)
        {
            this.authorizer.AssertCanDo(UserOrganizationActions.Delete, userOrganizationId);

            byte[] vers = System.Convert.FromBase64String(version);

            UserOrganization oldUserOrganization = this.userOrganizationsRepository.FindForUpdate(userOrganizationId, vers);

            this.userOrganizationsRepository.Remove(oldUserOrganization);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{userOrganizationId:int}/canDelete")]
        public ErrorsDO CanDeleteUserOrganization(int userOrganizationId)
        {
            this.authorizer.AssertCanDo(UserOrganizationActions.Delete, userOrganizationId);

            var errorList = this.userOrganizationsRepository.CanDeleteUserOrganization(userOrganizationId);

            return new ErrorsDO(errorList);
        }
    }
}
