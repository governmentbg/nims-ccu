using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/registrationStatuses")]
    public class ProjectRegistrationStatusNomsController : EnumNomsController<ProjectRegistrationStatus>
    {
        public ProjectRegistrationStatusNomsController(IEnumNomsRepository<ProjectRegistrationStatus> projectRegistrationStatusEnumNomsRepository)
            : base(projectRegistrationStatusEnumNomsRepository)
        {
        }
    }
}
