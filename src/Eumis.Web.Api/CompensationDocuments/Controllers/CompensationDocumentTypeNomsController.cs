using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CompensationDocuments.Controllers
{
    [RoutePrefix("api/nomenclatures/compensationDocumentTypes")]
    public class CompensationDocumentTypeNomsController : EnumNomsController<CompensationDocumentType>
    {
        public CompensationDocumentTypeNomsController(IEnumNomsRepository<CompensationDocumentType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
