using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialCSDs.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialCorrectionCSDStatuses")]
    public class ContractReportFinancialCorrectionCSDStatusNomsController : EnumNomsController<ContractReportFinancialCorrectionCSDStatus>
    {
        public ContractReportFinancialCorrectionCSDStatusNomsController(IEnumNomsRepository<ContractReportFinancialCorrectionCSDStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
