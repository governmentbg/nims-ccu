using Autofac.Features.AttributeFilters;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain.EvalSessions;

namespace Eumis.Authentication.Authorization.ClaimsContexts.EvalSession
{
    internal class EvalSessionClaimsContext : ClaimsContext, IEvalSessionClaimsContext
    {
        private int evalSessionId;

        private IClaimsCache claimsCache;
        private IEvalSessionsRepository evalSessionsRepository;

        public EvalSessionClaimsContext(
            int evalSessionId,
            [KeyFilter(ClaimsCaches.EvalSession)]IClaimsCache claimsCache,
            IEvalSessionsRepository evalSessionsRepository)
            : base(claimsCache)
        {
            this.evalSessionId = evalSessionId;
            this.claimsCache = claimsCache;
            this.evalSessionsRepository = evalSessionsRepository;
        }

        public int EvalSessionId
        {
            get
            {
                return this.evalSessionId;
            }
        }

        public EvalSessionStatus EvalSessionStatus
        {
            get
            {
                return this.GetClaim(
                    this.evalSessionId,
                    new ClaimKey("EvalSessionStatus"),
                    () => this.evalSessionsRepository.GetEvalSessionStatus(this.evalSessionId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.evalSessionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.evalSessionsRepository.GetProgrammeId(this.evalSessionId));
            }
        }
    }
}
