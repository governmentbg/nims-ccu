using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialCertCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialCertCorrectionCSDStatuses")]
    public class ContractReportFinancialCertCorrectionCSDStatusNomsController : EnumNomsController<ContractReportFinancialCertCorrectionCSDStatus>
    {
        public ContractReportFinancialCertCorrectionCSDStatusNomsController(IEnumNomsRepository<ContractReportFinancialCertCorrectionCSDStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
