using Autofac.Features.AttributeFilters;
using Eumis.Data.Procedures.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.Procedure
{
    internal class ProcedureClaimsContext : ClaimsContext, IProcedureClaimsContext
    {
        private int procedureId;

        private IClaimsCache claimsCache;
        private IProceduresRepository proceduresRepository;

        public ProcedureClaimsContext(
            int procedureId,
            [KeyFilter(ClaimsCaches.Procedure)]IClaimsCache claimsCache,
            IProceduresRepository proceduresRepository)
            : base(claimsCache)
        {
            this.procedureId = procedureId;
            this.claimsCache = claimsCache;
            this.proceduresRepository = proceduresRepository;
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.procedureId,
                    new ClaimKey("ProgrammeId"),
                    () => this.proceduresRepository.GetPrimaryProcedureProgrammeId(this.procedureId));
            }
        }
    }
}
