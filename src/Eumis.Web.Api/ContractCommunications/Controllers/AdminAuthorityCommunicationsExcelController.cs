using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;
using System.Web.Http;

namespace Eumis.Web.Api.ContractCommunications.Controllers
{
    [RoutePrefix("api/contracts/{contractId:int}/communications")]
    public class AdminAuthorityCommunicationsExcelController : ContractCommunicationsExcelController
    {
        private IAuthorizer authorizer;

        public AdminAuthorityCommunicationsExcelController(
            IAuthorizer authorizer,
            IContractsRepository contractsRepository,
            IContractCommunicationXmlsRepository contractCommunicationXmlsRepository)
            : base(
                ContractCommunicationType.Administrative,
                contractsRepository,
                contractCommunicationXmlsRepository)
        {
            this.authorizer = authorizer;
        }

        protected override void AssertPermissions(int contractId)
        {
            this.authorizer.AssertCanDo(ContractCommunicationListActions.Search);
        }
    }
}
