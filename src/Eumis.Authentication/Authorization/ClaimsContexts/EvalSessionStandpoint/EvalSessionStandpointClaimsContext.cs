using Autofac.Features.AttributeFilters;
using Eumis.Data.EvalSessions.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.EvalSessionStandpoint
{
    internal class EvalSessionStandpointClaimsContext : ClaimsContext, IEvalSessionStandpointClaimsContext
    {
        private int standpointId;
        private IClaimsCache claimsCache;
        private IEvalSessionsRepository evalSessionsRepository;

        public EvalSessionStandpointClaimsContext(
            int standpointId,
            [KeyFilter(ClaimsCaches.EvalSessionStandpoint)]IClaimsCache claimsCache,
            IEvalSessionsRepository evalSessionsRepository)
            : base(claimsCache)
        {
            this.standpointId = standpointId;
            this.claimsCache = claimsCache;
            this.evalSessionsRepository = evalSessionsRepository;
        }

        public int EvalSessionStandpointId
        {
            get
            {
                return this.standpointId;
            }
        }

        public int ProjectId
        {
            get
            {
                return this.GetClaim(
                    this.standpointId,
                    new ClaimKey("ProjectId"),
                    () => this.evalSessionsRepository.GetEvalSessionStandpointProjectId(this.standpointId));
            }
        }
    }
}
