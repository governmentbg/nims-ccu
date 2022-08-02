using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/applicationFormTypes")]
    public class ApplicationFormTypeNomsController : EnumNomsController<ApplicationFormType>
    {
        public ApplicationFormTypeNomsController(IEnumNomsRepository<ApplicationFormType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
