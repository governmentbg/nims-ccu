using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Companies;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.Companies.Controllers
{
    [RoutePrefix("api/nomenclatures/programmeCompanies")]
    public class ProgrammeCompanyNomsController : EntityNomsController<Company, EntityNomVO>
    {
        public ProgrammeCompanyNomsController(IEntityNomsRepository<Company, EntityNomVO> companyNomsRepository)
            : base(companyNomsRepository)
        {
        }
    }
}
