using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialCorrectionStatuses")]
    public class ContractReportFinancialCorrectionStatusNomsController : EnumNomsController<ContractReportFinancialCorrectionStatus>
    {
        public ContractReportFinancialCorrectionStatusNomsController(IEnumNomsRepository<ContractReportFinancialCorrectionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
