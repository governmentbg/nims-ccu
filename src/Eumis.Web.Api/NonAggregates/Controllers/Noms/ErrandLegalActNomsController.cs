using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers
{
    [RoutePrefix("api/nomenclatures/errandLegalActs")]
    public class ErrandLegalActNomsController : EntityGidNomsController<ErrandLegalAct, EntityGidNomVO>
    {
        public ErrandLegalActNomsController(IEntityGidNomsRepository<ErrandLegalAct, EntityGidNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
