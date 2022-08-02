using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialCSDs.Controllers
{
    [RoutePrefix("api/nomenclatures/costSupportingDocumentTypes")]
    public class CostSupportingDocumentTypesNomsController : EnumNomsController<CostSupportingDocumentType>
    {
        public CostSupportingDocumentTypesNomsController(IEnumNomsRepository<CostSupportingDocumentType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
