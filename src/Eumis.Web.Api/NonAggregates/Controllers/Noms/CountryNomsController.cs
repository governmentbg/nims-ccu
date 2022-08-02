using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/countries")]
    public class CountryNomsController : EntityNomsController<Country, EntityCodeNomVO>
    {
        public CountryNomsController(IEntityCodeNomsRepository<Country, EntityCodeNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
