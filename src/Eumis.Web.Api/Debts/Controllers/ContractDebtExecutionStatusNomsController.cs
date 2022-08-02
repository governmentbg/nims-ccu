using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Debts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Debts.Controllers
{
    [RoutePrefix("api/nomenclatures/contractDebtExecutionStatuses")]
    public class ContractDebtExecutionStatusNomsController : EnumNomsController<ContractDebtExecutionStatus>
    {
        public ContractDebtExecutionStatusNomsController(IEnumNomsRepository<ContractDebtExecutionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
