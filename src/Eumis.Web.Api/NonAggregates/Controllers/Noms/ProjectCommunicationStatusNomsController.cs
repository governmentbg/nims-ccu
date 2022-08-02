using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/projectCommunicationStatuses")]
    public class ProjectCommunicationStatusNomsController : EnumNomsController<ProjectCommunicationStatus>
    {
        public ProjectCommunicationStatusNomsController(IEnumNomsRepository<ProjectCommunicationStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
