using System.Web.Http;
using Eumis.Common.Auth;
using Eumis.Data.Contracts.Repositories;
using Eumis.PortalIntegration.Api.Portal.ContractRegistrations.DataObjects;

namespace Eumis.PortalIntegration.Api.Portal.ContractRegistrations.Controllers
{
    public class ContractAccessCodes1Controller : ApiController
    {
        private IContractAccessCodesRepository contractAccessCodesRepository;
        private IAccessContext accessContext;

        public ContractAccessCodes1Controller(
            IAccessContext accessContext,
            IContractAccessCodesRepository contractAccessCodesRepository)
        {
            this.accessContext = accessContext;
            this.contractAccessCodesRepository = contractAccessCodesRepository;
        }

        [HttpGet]
        [Route("api/accesscode/info")]
        public ContractAccessCodeDO GetContractRegistrationInfo()
        {
            var ac = this.contractAccessCodesRepository.Find(this.accessContext.ContractAccessCodeId);

            return new ContractAccessCodeDO(ac);
        }
    }
}
