using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCertCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportCertCorrectionTypes")]
    public class ContractReportCertCorrectionTypeNomsController : EnumNomsController<ContractReportCertCorrectionType>
    {
        public ContractReportCertCorrectionTypeNomsController(IEnumNomsRepository<ContractReportCertCorrectionType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
