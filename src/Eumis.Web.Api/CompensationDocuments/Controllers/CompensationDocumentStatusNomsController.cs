using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CompensationDocuments.Controllers
{
    [RoutePrefix("api/nomenclatures/compensationDocumentStatuses")]
    public class CompensationDocumentStatusNomsController : EnumNomsController<CompensationDocumentStatus>
    {
        public CompensationDocumentStatusNomsController(IEnumNomsRepository<CompensationDocumentStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
