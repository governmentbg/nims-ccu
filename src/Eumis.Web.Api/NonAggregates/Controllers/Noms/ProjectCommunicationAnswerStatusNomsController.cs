using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/projectCommunicationAnswerStatuses")]
    public class ProjectCommunicationAnswerStatusNomsController : EnumNomsController<ProjectCommunicationAnswerStatus>
    {
        public ProjectCommunicationAnswerStatusNomsController(IEnumNomsRepository<ProjectCommunicationAnswerStatus> nomsRepository)
       : base(nomsRepository)
        {
        }
    }
}
