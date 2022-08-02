using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialCSDs.Controllers
{
    [RoutePrefix("api/nomenclatures/costSupportingDocumentCompanyTypes")]
    public class CostSupportingDocumentCompanyTypesNomsController : EnumNomsController<CostSupportingDocumentCompanyType>
    {
        public CostSupportingDocumentCompanyTypesNomsController(IEnumNomsRepository<CostSupportingDocumentCompanyType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
