using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Core;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/currencies")]
    public class CurrencyNomsController : EnumNomsController<Currency>
    {
        public CurrencyNomsController(IEnumNomsRepository<Currency> currencyEnumNomsRepository)
            : base(currencyEnumNomsRepository)
        {
        }
    }
}
