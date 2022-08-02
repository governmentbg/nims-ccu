using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialCertCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialCertCorrectionStatuses")]
    public class ContractReportFinancialCertCorrectionStatusNomsController : EnumNomsController<ContractReportFinancialCertCorrectionStatus>
    {
        public ContractReportFinancialCertCorrectionStatusNomsController(IEnumNomsRepository<ContractReportFinancialCertCorrectionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
