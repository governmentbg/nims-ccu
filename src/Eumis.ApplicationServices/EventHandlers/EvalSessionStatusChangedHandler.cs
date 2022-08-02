using Eumis.Authentication.Authorization;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Events;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class EvalSessionStatusChangedHandler : Eumis.Domain.Core.EventHandler<EvalSessionStatusChangedEvent>
    {
        private ICacheManager cacheManager;

        public EvalSessionStatusChangedHandler(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public override void Handle(EvalSessionStatusChangedEvent e)
        {
            this.cacheManager.ClearCache(ClaimsCaches.EvalSession, e.EvalSessionId);
        }
    }
}
