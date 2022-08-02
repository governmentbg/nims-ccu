using DocumentFormat.OpenXml.Bibliography;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportTypes")]
    public class ContractReportTypeNomsController : ApiController
    {
        private IContractReportTypeNomsRepository contractReportTypeNomsRepository;

        public ContractReportTypeNomsController(IContractReportTypeNomsRepository contractReportTypeNomsRepository)
        {
            this.contractReportTypeNomsRepository = contractReportTypeNomsRepository;
        }

        [Route("{id}")]
        public EnumNomVO<ContractReportType> GetNom(ContractReportType id)
        {
            return this.contractReportTypeNomsRepository.GetNom(id);
        }

        [Route("")]
        public IList<EnumNomVO<ContractReportType>> GetNoms(string term = null)
        {
            return this.contractReportTypeNomsRepository.GetNoms(term);
        }

        [Route("")]
        public IList<EnumNomVO<ContractReportType>> GetNoms(int contractId, string term = null)
        {
            return this.contractReportTypeNomsRepository.GetNoms(contractId, term);
        }
    }
}
