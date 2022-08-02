using System.Collections.Generic;
using System.Web.Http;
using Eumis.Common.Auth;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Domain.Irregularities;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularitySignals")]
    public class IrregularitySignalNomsController : ApiController
    {
        private IIrregularitySignalNomsRepository irregularitySignalNomsRepository;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public IrregularitySignalNomsController(
            IIrregularitySignalNomsRepository irregularitySignalNomsRepository,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository)
        {
            this.irregularitySignalNomsRepository = irregularitySignalNomsRepository;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.irregularitySignalNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(string term = null, int offset = 0, int? limit = null, IrregularitySignalStatus? status = null, bool freeOnly = false)
        {
            var programmeIds = System.Array.Empty<int>();

            return this.irregularitySignalNomsRepository.GetSignalNoms(term, offset, limit, programmeIds, status, freeOnly);
        }

        [Route("")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public IEnumerable<EntityNomVO> GetRegisterNoms(bool isForRegister, string term = null, int offset = 0, int? limit = null)
        {
            var programmeIds = System.Array.Empty<int>();

            return this.irregularitySignalNomsRepository.GetRegisterSignalNoms(term, offset, limit, programmeIds);
        }
    }
}
