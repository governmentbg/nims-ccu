using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialCSDs.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialCorrectionCSDCertStatuses")]
    public class ContractReportFinancialCorrectionCSDCertStatusNomsController : EnumNomsController<ContractReportFinancialCorrectionCSDCertStatus>
    {
        public ContractReportFinancialCorrectionCSDCertStatusNomsController(IEnumNomsRepository<ContractReportFinancialCorrectionCSDCertStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
