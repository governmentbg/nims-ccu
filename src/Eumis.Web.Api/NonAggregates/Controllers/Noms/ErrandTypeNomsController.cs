using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Allowances;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.BasicInterestRates.Controllers
{
    [RoutePrefix("api/nomenclatures/errandTypes")]
    public class ErrandTypeNomsController : DependentEntityNomsController<ErrandType, EntityNomVO>
    {
        public ErrandTypeNomsController(IDependentEntityNomsRepository<ErrandType, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
