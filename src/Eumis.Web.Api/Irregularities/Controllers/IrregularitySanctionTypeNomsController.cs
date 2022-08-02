using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularitySanctionTypes")]
    public class IrregularitySanctionTypeNomsController : EntityNomsController<IrregularitySanctionType, IrregularitySanctionTypeVO>
    {
        private IIrregularitySanctionTypeNomsRepository irregularitySanctionTypeNomsRepository;

        public IrregularitySanctionTypeNomsController(IIrregularitySanctionTypeNomsRepository irregularitySanctionTypeNomsRepository)
            : base(irregularitySanctionTypeNomsRepository)
        {
            this.irregularitySanctionTypeNomsRepository = irregularitySanctionTypeNomsRepository;
        }

        [Route("")]
        public IEnumerable<IrregularitySanctionTypeVO> GetSanctionTypes(int sanctionCategoryId, string term = null, int offset = 0, int? limit = null)
        {
            return this.irregularitySanctionTypeNomsRepository.GetSanctionTypeNoms(sanctionCategoryId, term, offset, limit);
        }
    }
}
