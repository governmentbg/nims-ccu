using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityTypes")]
    public class IrregularityTypeNomsController : EntityNomsController<IrregularityType, IrregularityTypeVO>
    {
        private IIrregularityTypeNomsRepository irregularityTypeNomsRepository;

        public IrregularityTypeNomsController(IIrregularityTypeNomsRepository irregularityTypeNomsRepository)
            : base(irregularityTypeNomsRepository)
        {
            this.irregularityTypeNomsRepository = irregularityTypeNomsRepository;
        }

        [Route("")]
        public IEnumerable<IrregularityTypeVO> GetIrregularityTypes(int irregularityCategoryId, string term = null, int offset = 0, int? limit = null)
        {
            return this.irregularityTypeNomsRepository.GetIrregularityTypeNoms(irregularityCategoryId, term, offset, limit);
        }
    }
}
