using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/contractExecutionStatuses")]
    public class ContractExecutionStatusNomsController : EnumNomsController<ContractExecutionStatus>
    {
        public ContractExecutionStatusNomsController(IEnumNomsRepository<ContractExecutionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
