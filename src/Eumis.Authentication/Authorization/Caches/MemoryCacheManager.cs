using Autofac.Features.Indexed;

namespace Eumis.Authentication.Authorization
{
    public class MemoryCacheManager : ICacheManager
    {
        private IIndex<ClaimsCaches, IClaimsCache> claimsCaches;

        public MemoryCacheManager(IIndex<ClaimsCaches, IClaimsCache> claimsCaches)
        {
            this.claimsCaches = claimsCaches;
        }

        public void ClearCache(ClaimsCaches cache, int subjectId)
        {
            ((MemoryClaimsCache)this.claimsCaches[cache]).ClearCache(subjectId);
        }
    }
}
