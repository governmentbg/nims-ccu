using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.FinancialCorrections.Repositories;

namespace Eumis.Web.Api.FinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/financialCorrections")]
    public class FinancialCorrectionNomsController : ApiController
    {
        private IFinancialCorrectionNomsRepository financialCorrectionNomsRepository;

        public FinancialCorrectionNomsController(
            IFinancialCorrectionNomsRepository financialCorrectionNomsRepository)
        {
            this.financialCorrectionNomsRepository = financialCorrectionNomsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.financialCorrectionNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(int contractId, string term = null, int offset = 0, int? limit = null)
        {
            return this.financialCorrectionNomsRepository.GetNoms(contractId, term, offset, limit);
        }
    }
}
