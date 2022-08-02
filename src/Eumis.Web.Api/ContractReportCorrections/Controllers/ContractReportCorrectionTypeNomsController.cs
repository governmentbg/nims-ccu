using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportCorrectionTypes")]
    public class ContractReportCorrectionTypeNomsController : EnumNomsController<ContractReportCorrectionType>
    {
        public ContractReportCorrectionTypeNomsController(IEnumNomsRepository<ContractReportCorrectionType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
