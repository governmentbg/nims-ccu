using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/sebraPaymentTypes")]
    public class SebraPaymentTypeNomsController : EnumNomsController<SebraPaymentType>
    {
        public SebraPaymentTypeNomsController(IEnumNomsRepository<SebraPaymentType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
