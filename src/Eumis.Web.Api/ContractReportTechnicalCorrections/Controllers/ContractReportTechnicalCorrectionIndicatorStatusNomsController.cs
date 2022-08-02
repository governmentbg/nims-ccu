using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportTechnicalCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportTechnicalCorrectionIndicatorStatuses")]
    public class ContractReportTechnicalCorrectionIndicatorStatusNomsController : EnumNomsController<ContractReportTechnicalCorrectionIndicatorStatus>
    {
        public ContractReportTechnicalCorrectionIndicatorStatusNomsController(IEnumNomsRepository<ContractReportTechnicalCorrectionIndicatorStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
