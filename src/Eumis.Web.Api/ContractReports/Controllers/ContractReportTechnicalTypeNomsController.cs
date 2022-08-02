using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportTechnicalTypes")]
    public class ContractReportTechnicalTypeNomsController : EnumNomsController<ContractReportTechnicalType>
    {
        public ContractReportTechnicalTypeNomsController(IEnumNomsRepository<ContractReportTechnicalType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
