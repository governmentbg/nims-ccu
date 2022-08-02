using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.Authorization.ClaimsContexts
{
    internal class ClaimsContext
    {
        private IClaimsCache claimsCache;

        public ClaimsContext(IClaimsCache claimsCache)
        {
            this.claimsCache = claimsCache;
        }

        protected TValue GetClaim<TValue>(int subjectId, ClaimKey claimKey, Func<TValue> getter)
        {
            if (this.claimsCache.TryGetCachedClaim(subjectId, claimKey, out TValue cached))
            {
                return cached;
            }

            var value = getter();

            this.claimsCache.AddClaim(subjectId, claimKey, value);

            return value;
        }
    }
}
