using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Domain.Projects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/projectVersionFiles")]
    public class ProjectVersionFileNomsController : EntityNomsController<ProjectVersionXmlFile, EntityNomVO>
    {
        private IProjectVersionXmlFileNomsRepository projectVersionXmlFileNomsRepository;

        public ProjectVersionFileNomsController(IProjectVersionXmlFileNomsRepository projectVersionXmlFileNomsRepository)
            : base(projectVersionXmlFileNomsRepository)
        {
            this.projectVersionXmlFileNomsRepository = projectVersionXmlFileNomsRepository;
        }

        [Route("")]
        public IList<EntityNomVO> GetProjectVersionXmlFiles(int projectVersionXmlId, string term = null, int offset = 0, int? limit = null)
        {
            return this.projectVersionXmlFileNomsRepository.GetNomsForProjectVersion(projectVersionXmlId, term, offset, limit);
        }
    }
}
