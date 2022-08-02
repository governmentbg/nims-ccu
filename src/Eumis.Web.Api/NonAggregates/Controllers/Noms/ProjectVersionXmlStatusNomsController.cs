using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/projectVersionXmlStatus")]
    public class ProjectVersionXmlStatusNomsController : EnumNomsController<ProjectVersionXmlStatus>
    {
        public ProjectVersionXmlStatusNomsController(IEnumNomsRepository<ProjectVersionXmlStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
