using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Data;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.NonAggregates.Repositories;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/institutions")]
    public class InstitutionNomsController : EntityNomsController<Institution, EntityNomVO>
    {
        private IInstitutionNomsRepository institutionNomsRepository;

        public InstitutionNomsController(IInstitutionNomsRepository institutionNomsRepository)
            : base(institutionNomsRepository)
        {
            this.institutionNomsRepository = institutionNomsRepository;
        }

        [Route("")]
        public IList<EntityNomVO> GetUnusedProgrammeInstitutions(int programmeId, string term = null, int offset = 0, int? limit = null)
        {
            return this.institutionNomsRepository.GetInstitutionNoms(programmeId, term, offset, limit);
        }
    }
}
