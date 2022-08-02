using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Auth;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Web.Api.Contracts.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractAccessCodes.Controllers
{
    [RoutePrefix("api/contractAccessCodes")]
    public class ContractAccessCodesController : ApiController
    {
        private IAuthorizer authorizer;
        private IUserClaimsContext currentUserClaimsContext;
        private IContractAccessCodesRepository contractAccessCodesRepository;

        public ContractAccessCodesController(
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractAccessCodesRepository contractAccessCodesRepository,
            UserClaimsContextFactory userClaimsContextFactory)
        {
            this.authorizer = authorizer;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }

            this.contractAccessCodesRepository = contractAccessCodesRepository;
        }

        [Route("")]
        public IList<ContractAccessCodeVO> GetContractAccessCodes()
        {
            this.authorizer.AssertCanDo(ContractAccessCodeListActions.Search);

            return this.contractAccessCodesRepository.GetContractAccessCodes(this.currentUserClaimsContext.IsSuperUser);
        }

        [Route("{accessCodeId:int}")]
        public ContractAccessCodeDO GetContractAccessCode(int accessCodeId)
        {
            var contractId = this.contractAccessCodesRepository.GetContractId(accessCodeId);

            this.authorizer.AssertCanDo(ContractAccessCodeActions.View, contractId);

            var accessCode = this.contractAccessCodesRepository.Find(accessCodeId);

            return new ContractAccessCodeDO(accessCode, this.currentUserClaimsContext.IsSuperUser);
        }

        [Route("info")]
        public object GetAccessCodesInfo()
        {
            this.authorizer.AssertCanDo(ContractAccessCodeListActions.Search);

            return new { ShowAccessCodes = this.currentUserClaimsContext.IsSuperUser };
        }
    }
}
