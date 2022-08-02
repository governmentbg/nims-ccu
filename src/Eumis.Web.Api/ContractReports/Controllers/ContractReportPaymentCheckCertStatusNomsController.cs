using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportPaymentCheckCertStatuses")]
    public class ContractReportPaymentCheckCertStatusNomsController : EnumNomsController<ContractReportPaymentCheckCertStatus>
    {
        public ContractReportPaymentCheckCertStatusNomsController(IEnumNomsRepository<ContractReportPaymentCheckCertStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
