using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.Authorization
{
    public interface IClaimsCache
    {
        bool TryGetCachedClaim<TValue>(int subjectId, ClaimKey claimKey, out TValue value);

        void AddClaim<TValue>(int subjectId, ClaimKey claimKey, TValue value);
    }
}
