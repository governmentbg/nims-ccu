using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/companyTypes")]
    public class CompanyTypeNomsController : EntityNomsController<CompanyType, CompanyTypeGidNomVO>
    {
        public CompanyTypeNomsController(IEntityGidNomsRepository<CompanyType, CompanyTypeGidNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
