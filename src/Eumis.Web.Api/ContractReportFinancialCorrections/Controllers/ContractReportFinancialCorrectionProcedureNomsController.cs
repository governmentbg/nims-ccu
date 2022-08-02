using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Nomenclatures;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialCorrectionProcedures")]
    public class ContractReportFinancialCorrectionProcedureNomsController : ApiController
    {
        private IContractReportProcedureNomsRepository contractReportProcedureNomsRepository;

        public ContractReportFinancialCorrectionProcedureNomsController(
            IContractReportProcedureNomsRepository contractReportProcedureNomsRepository)
        {
            this.contractReportProcedureNomsRepository = contractReportProcedureNomsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.contractReportProcedureNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(string term = null, int offset = 0, int? limit = null)
        {
            return this.contractReportProcedureNomsRepository.GetNoms(term, offset, limit);
        }
    }
}
