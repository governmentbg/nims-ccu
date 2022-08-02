using System.Collections.Generic;
using System.Web.Http;
using Eumis.Common.Auth;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/massCommunicationProgrammes")]
    public class ProcedureMassCommunicationProgrammeNomsController : ApiController
    {
        private IProgrammeNomsRepository programmeNomsRepository;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public ProcedureMassCommunicationProgrammeNomsController(
            IProgrammeNomsRepository programmeNomsRepository,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository)
        {
            this.programmeNomsRepository = programmeNomsRepository;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.programmeNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(string term = null, int offset = 0, int? limit = null)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractPermissions.CanWrite);

            return this.programmeNomsRepository.GetProgrammeNoms(term, offset, limit, programmeIds: programmeIds);
        }
    }
}
