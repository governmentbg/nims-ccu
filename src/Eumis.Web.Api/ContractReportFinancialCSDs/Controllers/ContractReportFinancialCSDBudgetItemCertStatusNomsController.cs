using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialCSDs.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialCSDBudgetItemCertStatuses")]
    public class ContractReportFinancialCSDBudgetItemCertStatusNomsController : EnumNomsController<ContractReportFinancialCSDBudgetItemCertStatus>
    {
        public ContractReportFinancialCSDBudgetItemCertStatusNomsController(IEnumNomsRepository<ContractReportFinancialCSDBudgetItemCertStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
