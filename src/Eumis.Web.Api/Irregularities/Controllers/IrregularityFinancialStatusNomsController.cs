using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityFinancialStatuses")]
    public class IrregularityFinancialStatusNomsController : EntityNomsController<IrregularityFinancialStatus, EntityCodeNomVO>
    {
        public IrregularityFinancialStatusNomsController(IEntityCodeNomsRepository<IrregularityFinancialStatus, EntityCodeNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
