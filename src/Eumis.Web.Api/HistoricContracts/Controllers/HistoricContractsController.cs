using Eumis.ApplicationServices.Services.HistoricContract;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Auth;
using Eumis.Data.HistoricContract.Repositories;
using Eumis.Data.HistoricContract.ViewObjects;
using Eumis.Domain.HistoricContracts.DataObjects;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.HistoricContracts.Controllers
{
    [RoutePrefix("api/historicContracts")]
    public class HistoricContractsController : ApiController
    {
        private IHistoricContractService historicContractService;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IHistoricContractRequestRepository historicContractRepository;

        public HistoricContractsController(
            IHistoricContractService historicContractService,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IHistoricContractRequestRepository historicContractRepository)
        {
            this.historicContractService = historicContractService;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.historicContractRepository = historicContractRepository;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage UpdateHistoricContracts(List<HistoricContractDO> historicContracts)
        {
            if (!this.accessContext.IsExternalSystem || this.accessContext.ExternalSystemProperty != "iacs_service")
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            var errorList = this.historicContractService.UpdateHistoricContracts(historicContracts);

            if (errorList.Any())
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, errorList);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [Route("")]
        public IList<HistoricContractRequestVO> GetHistoricContractRequests()
        {
            this.authorizer.AssertCanDo(InterfacesActions.Export);

            return this.historicContractRepository.GetHistoricContractRequests();
        }

        [Route("{historicContractRequestId:int}")]
        public HistoricContractRequestInfoVO GetHistoricContractRequestData(int historicContractRequestId)
        {
            this.authorizer.AssertCanDo(InterfacesActions.Export);

            return this.historicContractRepository.GetHistoricContractRequestInfo(historicContractRequestId);
        }
    }
}
