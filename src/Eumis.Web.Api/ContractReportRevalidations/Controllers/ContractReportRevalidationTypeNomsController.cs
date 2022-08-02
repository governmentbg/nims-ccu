using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidations.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportRevalidationTypes")]
    public class ContractReportRevalidationTypeNomsController : EnumNomsController<ContractReportRevalidationType>
    {
        public ContractReportRevalidationTypeNomsController(IEnumNomsRepository<ContractReportRevalidationType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
