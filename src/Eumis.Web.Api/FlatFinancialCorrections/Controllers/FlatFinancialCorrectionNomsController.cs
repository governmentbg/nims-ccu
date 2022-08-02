using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.FlatFinancialCorrections.Repositories;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/flatFinancialCorrections")]
    public class FlatFinancialCorrectionNomsController : ApiController
    {
        private IFlatFinancialCorrectionNomsRepository flatFinancialCorrectionNomsRepository;

        public FlatFinancialCorrectionNomsController(
            IFlatFinancialCorrectionNomsRepository flatFinancialCorrectionNomsRepository)
        {
            this.flatFinancialCorrectionNomsRepository = flatFinancialCorrectionNomsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.flatFinancialCorrectionNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(string term = null, int offset = 0, int? limit = null)
        {
            return this.flatFinancialCorrectionNomsRepository.GetNoms(term, offset, limit);
        }
    }
}
