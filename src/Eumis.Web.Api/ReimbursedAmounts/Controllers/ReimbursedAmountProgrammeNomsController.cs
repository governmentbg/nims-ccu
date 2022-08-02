using System.Collections.Generic;
using System.Web.Http;
using Eumis.Common.Auth;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.Web.Api.ReimbursedAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/reimbursedAmountProgrammes")]
    public class ReimbursedAmountProgrammeNomsController : ApiController
    {
        private IProgrammeNomsRepository programmeNomsRepository;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public ReimbursedAmountProgrammeNomsController(
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
        public IEnumerable<EntityNomVO> GetNoms(int? procedureId = null, string term = null, int offset = 0, int? limit = null)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial);

            return this.programmeNomsRepository.GetProgrammeNoms(term, offset, limit, procedureId: procedureId, programmeIds: programmeIds);
        }
    }
}
