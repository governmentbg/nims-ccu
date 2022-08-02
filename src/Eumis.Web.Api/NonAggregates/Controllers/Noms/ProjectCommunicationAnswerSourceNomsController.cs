using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/projectCommunicationAnswerSources")]
    public class ProjectCommunicationAnswerSourceNomsController : EnumNomsController<ProjectCommunicationAnswerSource>
    {
        public ProjectCommunicationAnswerSourceNomsController(IEnumNomsRepository<ProjectCommunicationAnswerSource> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
