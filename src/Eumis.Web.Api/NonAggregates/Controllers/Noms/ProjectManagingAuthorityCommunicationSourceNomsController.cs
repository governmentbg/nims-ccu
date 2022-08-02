using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/projectManagingAuthorityCommunicationSources")]
    public class ProjectManagingAuthorityCommunicationSourceNomsController : EnumNomsController<ProjectManagingAuthorityCommunicationSource>
    {
        public ProjectManagingAuthorityCommunicationSourceNomsController(IEnumNomsRepository<ProjectManagingAuthorityCommunicationSource> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
