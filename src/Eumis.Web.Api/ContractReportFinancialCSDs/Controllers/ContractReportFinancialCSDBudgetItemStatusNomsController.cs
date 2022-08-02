using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialCSDs.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialCSDBudgetItemStatuses")]
    public class ContractReportFinancialCSDBudgetItemStatusNomsController : EnumNomsController<ContractReportFinancialCSDBudgetItemStatus>
    {
        public ContractReportFinancialCSDBudgetItemStatusNomsController(IEnumNomsRepository<ContractReportFinancialCSDBudgetItemStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
