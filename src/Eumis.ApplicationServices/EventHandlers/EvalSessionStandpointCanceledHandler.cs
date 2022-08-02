using Eumis.Authentication.Authorization;
using Eumis.Domain.Events;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class EvalSessionStandpointCanceledHandler : Eumis.Domain.Core.EventHandler<EvalSessionStandpointCanceledEvent>
    {
        private ICacheManager cacheManager;

        public EvalSessionStandpointCanceledHandler(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public override void Handle(EvalSessionStandpointCanceledEvent e)
        {
            this.cacheManager.ClearCache(ClaimsCaches.User, e.EvalSessionStandpointUserId);
        }
    }
}
