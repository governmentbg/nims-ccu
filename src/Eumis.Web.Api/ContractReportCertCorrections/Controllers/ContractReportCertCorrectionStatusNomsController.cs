using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCertCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportCertCorrectionStatuses")]
    public class ContractReportCertCorrectionStatusNomsController : EnumNomsController<ContractReportCertCorrectionStatus>
    {
        public ContractReportCertCorrectionStatusNomsController(IEnumNomsRepository<ContractReportCertCorrectionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
