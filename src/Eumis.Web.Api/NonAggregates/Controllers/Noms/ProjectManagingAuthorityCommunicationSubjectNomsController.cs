using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/projectManagingAuthorityCommunicationSubjects")]
    public class ProjectManagingAuthorityCommunicationSubjectNomsController : EnumNomsController<ProjectManagingAuthorityCommunicationSubject>
    {
        public ProjectManagingAuthorityCommunicationSubjectNomsController(IEnumNomsRepository<ProjectManagingAuthorityCommunicationSubject> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
