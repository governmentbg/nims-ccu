using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/projectRecieveTypes")]
    public class ProjectRecieveTypeNomsController : EnumNomsController<ProjectRecieveType>
    {
        public ProjectRecieveTypeNomsController(IEnumNomsRepository<ProjectRecieveType> projectRecieveTypeEnumNomsRepository)
            : base(projectRecieveTypeEnumNomsRepository)
        {
        }
    }
}
