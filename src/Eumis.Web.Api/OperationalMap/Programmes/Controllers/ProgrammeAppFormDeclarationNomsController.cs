using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/nomenclatures/programmeAppFormDeclarations")]
    public class ProgrammeAppFormDeclarationNomsController : EntityNomsController<ProgrammeAppFormDeclaration, EntityNomVO>
    {
        private IProgrammeAppFormDeclarationNomsRepository programmeAppFormDeclarationNomsRepository;

        public ProgrammeAppFormDeclarationNomsController(IProgrammeAppFormDeclarationNomsRepository programmeAppFormDeclarationNomsRepository)
            : base(programmeAppFormDeclarationNomsRepository)
        {
            this.programmeAppFormDeclarationNomsRepository = programmeAppFormDeclarationNomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetExternalDocuments(int programmeId, int procedureId, string term = null, int offset = 0, int? limit = null)
        {
            return this.programmeAppFormDeclarationNomsRepository.GetDeclarations(programmeId, procedureId, term, offset, limit);
        }
    }
}
