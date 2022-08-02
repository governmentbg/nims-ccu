using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.BasicInterestRates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.BasicInterestRates.Controllers
{
    [RoutePrefix("api/nomenclatures/basicInterestRates")]
    public class BasicInterestRateNomsController : EntityNomsController<BasicInterestRate, EntityNomVO>
    {
        public BasicInterestRateNomsController(IEntityNomsRepository<BasicInterestRate, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
