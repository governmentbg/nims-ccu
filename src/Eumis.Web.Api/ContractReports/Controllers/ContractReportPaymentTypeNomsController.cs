using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportPaymentTypes")]
    public class ContractReportPaymentTypeNomsController : EnumNomsController<ContractReportPaymentType>
    {
        public ContractReportPaymentTypeNomsController(IEnumNomsRepository<ContractReportPaymentType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
