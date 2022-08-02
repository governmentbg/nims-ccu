using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers
{
    [RoutePrefix("api/nomenclatures/errandAreas")]
    public class ErrandAreaNomsController : EntityNomsController<ErrandArea, EntityCodeNomVO>
    {
        public ErrandAreaNomsController(IEntityCodeNomsRepository<ErrandArea, EntityCodeNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
