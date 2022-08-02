using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Debts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Debts.Controllers
{
    [RoutePrefix("api/nomenclatures/contractDebtCertStatuses")]
    public class ContractDebtCertStatusNomsController : EnumNomsController<ContractDebtCertStatus>
    {
        public ContractDebtCertStatusNomsController(IEnumNomsRepository<ContractDebtCertStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
