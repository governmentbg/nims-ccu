using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/kidCodes")]
    public class KidCodeNomsController : EntityNomsController<KidCode, EntityCodeNomVO>
    {
        public KidCodeNomsController(IEntityCodeNomsRepository<KidCode, EntityCodeNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
