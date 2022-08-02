using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractRegistrations.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Contracts.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/{contractId:int}/users")]
    public class ContractUsersController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private ICacheManager cacheManager;

        public ContractUsersController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            ICacheManager cacheManager)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.cacheManager = cacheManager;
        }

        [HttpGet]
        [Route("")]
        public IList<ContractUserVO> GetContractUsers(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.contractsRepository.GetContractUsers(contractId);
        }

        [HttpGet]
        [Route("{contractUserId:int}")]
        public ContractUserDO GetContractUser(int contractId, int contractUserId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            var contract = this.contractsRepository.Find(contractId);

            var contractUser = contract.FindContractUser(contractUserId);

            return new ContractUserDO(contractUser, contract.Version);
        }

        [HttpGet]
        [Route("new")]
        public ContractUserDO NewContractUser(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var version = this.contractsRepository.GetVersion(contractId);

            return new ContractUserDO(contractId, version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Users.Create), IdParam = "contractId")]
        public void AddContractUser(int contractId, ContractUserDO contractUser)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var contract = this.contractsRepository.FindForUpdate(contractId, contractUser.Version);

            contract.AddContractUser(contractUser.UserId.Value);

            this.unitOfWork.Save();

            this.cacheManager.ClearCache(ClaimsCaches.User, contractUser.UserId.Value);
        }

        [HttpDelete]
        [Transaction]
        [Route("{contractUserId:int}")]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Users.Delete), IdParam = "contractId", ChildIdParam = "contractUserId")]
        public void DeleteContractUser(int contractId, int contractUserId, string version)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            byte[] vers = System.Convert.FromBase64String(version);

            var contract = this.contractsRepository.FindForUpdate(contractId, vers);
            var userId = contract.GetUserId(contractUserId);

            contract.RemoveContractUser(contractUserId);

            this.unitOfWork.Save();

            this.cacheManager.ClearCache(ClaimsCaches.User, userId);
        }
    }
}
