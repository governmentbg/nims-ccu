using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/contractSpendingPlanStatuses")]
    public class ContractSpendingPlanStatusNomsController : EnumNomsController<ContractSpendingPlanStatus>
    {
        public ContractSpendingPlanStatusNomsController(IEnumNomsRepository<ContractSpendingPlanStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
