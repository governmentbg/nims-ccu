using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/contractProcurementStatuses")]
    public class ContractProcurementStatusNomsController : EnumNomsController<ContractProcurementStatus>
    {
        public ContractProcurementStatusNomsController(IEnumNomsRepository<ContractProcurementStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
