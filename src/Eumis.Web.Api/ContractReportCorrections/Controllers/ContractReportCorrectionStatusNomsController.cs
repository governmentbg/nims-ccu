using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportCorrectionStatuses")]
    public class ContractReportCorrectionStatusNomsController : EnumNomsController<ContractReportCorrectionStatus>
    {
        public ContractReportCorrectionStatusNomsController(IEnumNomsRepository<ContractReportCorrectionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
