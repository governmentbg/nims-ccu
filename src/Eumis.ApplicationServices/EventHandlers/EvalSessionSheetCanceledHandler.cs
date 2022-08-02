using Eumis.Authentication.Authorization;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Events;

namespace Eumis.ApplicationServices.EventHandlers
{
    public class EvalSessionSheetCanceledHandler : Eumis.Domain.Core.EventHandler<EvalSessionSheetCanceledEvent>
    {
        private ICacheManager cacheManager;

        public EvalSessionSheetCanceledHandler(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public override void Handle(EvalSessionSheetCanceledEvent e)
        {
            this.cacheManager.ClearCache(ClaimsCaches.User, e.EvalSessionSheetUserId);
        }
    }
}
