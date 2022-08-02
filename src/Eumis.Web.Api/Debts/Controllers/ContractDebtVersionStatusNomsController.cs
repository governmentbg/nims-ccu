using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Debts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Debts.Controllers
{
    [RoutePrefix("api/nomenclatures/contractDebtVersionStatuses")]
    public class ContractDebtVersionStatusNomsController : EnumNomsController<ContractDebtVersionStatus>
    {
        public ContractDebtVersionStatusNomsController(IEnumNomsRepository<ContractDebtVersionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
