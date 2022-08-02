using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CompensationDocuments.Controllers
{
    [RoutePrefix("api/nomenclatures/compensationDocumentSigns")]
    public class CompensationDocumentSignNomsController : EnumNomsController<CompensationSign>
    {
        public CompensationDocumentSignNomsController(IEnumNomsRepository<CompensationSign> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
