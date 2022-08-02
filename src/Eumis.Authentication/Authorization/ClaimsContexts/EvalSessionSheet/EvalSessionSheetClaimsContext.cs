using Autofac.Features.AttributeFilters;
using Eumis.Data.EvalSessions.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.EvalSessionSheet
{
    internal class EvalSessionSheetClaimsContext : ClaimsContext, IEvalSessionSheetClaimsContext
    {
        private int evalSessionSheetId;

        private IClaimsCache claimsCache;
        private IEvalSessionsRepository evalSessionsRepository;

        public EvalSessionSheetClaimsContext(
            int evalSessionSheetId,
            [KeyFilter(ClaimsCaches.EvalSessionSheet)]IClaimsCache claimsCache,
            IEvalSessionsRepository evalSessionsRepository)
            : base(claimsCache)
        {
            this.evalSessionSheetId = evalSessionSheetId;
            this.claimsCache = claimsCache;
            this.evalSessionsRepository = evalSessionsRepository;
        }

        public int EvalSessionSheetId
        {
            get
            {
                return this.evalSessionSheetId;
            }
        }
    }
}
