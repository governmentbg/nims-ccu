using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/budgetLevels")]
    public class BudgetLevelNomsController : EnumNomsController<BudgetLevel>
    {
        public BudgetLevelNomsController(IEnumNomsRepository<BudgetLevel> budgetLevelEnumNomsRepository)
            : base(budgetLevelEnumNomsRepository)
        {
        }
    }
}
