using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Domain.Projects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/projectDossierDocumentTypes")]
    public class ProjectDossierDocumentTypeNomsController : ApiController
    {
        private IProjectDossierDocumentTypeEnumNomsRepository nomsRepository;

        public ProjectDossierDocumentTypeNomsController(IProjectDossierDocumentTypeEnumNomsRepository nomsRepository)
        {
            this.nomsRepository = nomsRepository;
        }

        [Route("{id}")]
        public EnumNomVO<ProjectDossierDocumentType> GetNom(ProjectDossierDocumentType id)
        {
            return this.nomsRepository.GetNom(id);
        }

        [Route("")]
        public IList<EnumNomVO<ProjectDossierDocumentType>> GetNoms([FromUri]ProjectDossierDocumentType[] ids, string term = null)
        {
            return this.nomsRepository.GetNoms(ids, term);
        }
    }
}
