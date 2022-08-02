using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialCheckStatuses")]
    public class ContractReportFinancialCheckStatusNomsController : EnumNomsController<ContractReportFinancialCheckStatus>
    {
        public ContractReportFinancialCheckStatusNomsController(IEnumNomsRepository<ContractReportFinancialCheckStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
