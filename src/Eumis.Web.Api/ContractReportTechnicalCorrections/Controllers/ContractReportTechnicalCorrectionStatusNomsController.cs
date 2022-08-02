using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportTechnicalCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportTechnicalCorrectionStatuses")]
    public class ContractReportTechnicalCorrectionStatusNomsController : EnumNomsController<ContractReportTechnicalCorrectionStatus>
    {
        public ContractReportTechnicalCorrectionStatusNomsController(IEnumNomsRepository<ContractReportTechnicalCorrectionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
