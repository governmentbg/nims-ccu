using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/nomenclatures/nsiDeclarations")]
    public class NSIProgrammeDeclarationNomsController : ApiController
    {
        private IProgrammeAppFormDeclarationNomsRepository programmeAppFormDeclarationNomsRepository;

        public NSIProgrammeDeclarationNomsController(IProgrammeAppFormDeclarationNomsRepository programmeAppFormDeclarationNomsRepository)
        {
            this.programmeAppFormDeclarationNomsRepository = programmeAppFormDeclarationNomsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.programmeAppFormDeclarationNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNSIProgrammeDeclarations(int providedId, bool isAutomaticRequest = false, string term = null, int offset = 0, int? limit = null)
        {
            if (isAutomaticRequest)
            {
                return this.programmeAppFormDeclarationNomsRepository.GetNSIProgrammeDeclarations(providedId, term, offset, limit);
            }

            return this.programmeAppFormDeclarationNomsRepository.GetNSIProgrammeDeclarationsFromProjectVersionXML(providedId, term, offset, limit);
        }
    }
}
