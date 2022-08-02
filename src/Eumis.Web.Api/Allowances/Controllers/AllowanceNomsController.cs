using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Allowances;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.BasicInterestRates.Controllers
{
    [RoutePrefix("api/nomenclatures/allowances")]
    public class AllowanceNomsController : EntityNomsController<Allowance, EntityNomVO>
    {
        public AllowanceNomsController(IEntityNomsRepository<Allowance, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
