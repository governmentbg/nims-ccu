using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/projectTypes")]
    public class ProjectTypeNomsController : EntityNomsController<ProjectType, EntityNomVO>
    {
        public ProjectTypeNomsController(IProjectTypeNomsRepository nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
