using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportPayments")]
    public class ContractReportPaymentNomsController : ApiController
    {
        private IContractReportPaymentNomsRepository contractReportPaymentNomsRepository;

        public ContractReportPaymentNomsController(
            IContractReportPaymentNomsRepository contractReportPaymentNomsRepository)
        {
            this.contractReportPaymentNomsRepository = contractReportPaymentNomsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.contractReportPaymentNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(int contractId, [FromUri]int[] ids, string term = null, int offset = 0, int? limit = null)
        {
            return this.contractReportPaymentNomsRepository.GetNoms(contractId, ids, term, offset, limit);
        }
    }
}
