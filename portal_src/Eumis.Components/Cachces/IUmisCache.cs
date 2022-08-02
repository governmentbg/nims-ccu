using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Components.Caches
{
    public interface IEumisCache
    {
        bool TryGetCachedKey<TValue>(string subjectId, CacheKey cacheKey, out TValue value);

        void AddKey<TValue>(string subjectId, CacheKey cacheKey, TValue value);

        TValue GetKey<TValue>(string subjectId, CacheKey cacheKey, Func<TValue> getter);

        void ClearCache(string subjectId);
    }
}
