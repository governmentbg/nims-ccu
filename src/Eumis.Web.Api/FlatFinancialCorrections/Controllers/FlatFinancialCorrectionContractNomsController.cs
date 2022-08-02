using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/flatFinancialCorrectionContracts")]
    public class FlatFinancialCorrectionContractNomsController : EntityNomsController<Contract, EntityNomVO>
    {
        private IFlatFinancialCorrectionContractNomsRepository nomsRepository;

        public FlatFinancialCorrectionContractNomsController(IFlatFinancialCorrectionContractNomsRepository nomsRepository)
            : base(nomsRepository)
        {
            this.nomsRepository = nomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetContracts(int programmeId, string term = null, int offset = 0, int? limit = null)
        {
            return this.nomsRepository.GetContractNoms(new int[] { programmeId }, term, offset, limit);
        }
    }
}
