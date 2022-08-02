using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Relations;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Contracts.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/{contractId:int}/contractAccessCodes")]
    public class ContractContractAccessCodesController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IUserClaimsContext currentUserClaimsContext;
        private IContractAccessCodesRepository contractAccessCodesRepository;
        private IContractsRepository contractsRepository;
        private IAccessContext accessContext;
        private IActionLogger actionLogger;

        public ContractContractAccessCodesController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IActionLogger actionLogger,
            IContractAccessCodesRepository contractAccessCodesRepository,
            IContractsRepository contractsRepository,
            IRelationsRepository relationsRepository,
            UserClaimsContextFactory userClaimsContextFactory)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }

            this.actionLogger = actionLogger;
            this.contractAccessCodesRepository = contractAccessCodesRepository;
            this.contractsRepository = contractsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractAccessCodeVO> GetContractAccessCodes(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.contractAccessCodesRepository.GetContractAccessCodes(contractId, this.currentUserClaimsContext.IsSuperUser);
        }

        [Route("{accessCodeId:int}")]
        public ContractAccessCodeDO GetContractAccessCode(int contractId, int accessCodeId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);
            this.relationsRepository.AssertContractHasAccessCode(contractId, accessCodeId);

            var accessCode = this.contractAccessCodesRepository.Find(accessCodeId);

            return new ContractAccessCodeDO(accessCode, this.currentUserClaimsContext.IsSuperUser);
        }

        [HttpPost]
        [Route("{accessCodeId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.AccessCodes.Activate), IdParam = "contractId", ChildIdParam = "accessCodeId")]
        public void ActivateContractRegistration(int contractId, int accessCodeId, string version)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);
            this.relationsRepository.AssertContractHasAccessCode(contractId, accessCodeId);

            byte[] vers = System.Convert.FromBase64String(version);
            var accessCode = this.contractAccessCodesRepository.FindForUpdate(accessCodeId, vers);

            accessCode.Activate();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{accessCodeId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.AccessCodes.Deactivate), IdParam = "contractId", ChildIdParam = "accessCodeId")]
        public void DeactivateContractRegistration(int contractId, int accessCodeId, string version)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);
            this.relationsRepository.AssertContractHasAccessCode(contractId, accessCodeId);

            byte[] vers = System.Convert.FromBase64String(version);
            var accessCode = this.contractAccessCodesRepository.FindForUpdate(accessCodeId, vers);

            accessCode.Deactivate();

            this.unitOfWork.Save();
        }

        [Route("info")]
        public object GetAccessCodeInfo(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return new { ShowAccessCodes = this.currentUserClaimsContext.IsSuperUser };
        }
    }
}
