using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Components.Caches 
{
    public class EumisCache : IEumisCache
    {
        private ConcurrentDictionary<string, ConcurrentDictionary<string, object>> cache = new ConcurrentDictionary<string, ConcurrentDictionary<string, object>>();
        private ConcurrentDictionary<string, DateTime> cacheExpiration = new ConcurrentDictionary<string, DateTime>();

        public bool TryGetCachedKey<TValue>(string subjectId, CacheKey cacheKey, out TValue value)
        {
            if (this.cache.TryGetValue(subjectId, out var accessorClaims))
            {
                if (accessorClaims.TryGetValue(cacheKey.Key, out var storedValue))
                {
                    if (this.cacheExpiration.TryGetValue(subjectId, out var createdDate))
                    {
                        if (createdDate.AddMinutes(2) > DateTime.Now)
                        {
                            value = (TValue)storedValue;
                            return true;
                        }

                        this.ClearCache(subjectId);
                    }
                }
            }

            value = default(TValue);
            return false;
        }

        public void AddKey<TValue>(string subjectId, CacheKey cacheKey, TValue value)
        {
            var expirationClaim = this.cacheExpiration.GetOrAdd(subjectId, (a) => DateTime.Now);

            var accessorClaims = this.cache.GetOrAdd(subjectId, (a) => new ConcurrentDictionary<string, object>());
            accessorClaims.AddOrUpdate(cacheKey.Key, value, (k, v) => v);
        }

        public void ClearCache(string subjectId)
        {
            this.cache.TryRemove(subjectId, out var accessorClaims);
            this.cacheExpiration.TryRemove(subjectId, out DateTime createdDate);
        }

        public TValue GetKey<TValue>(string subjectId, CacheKey cacheKey, Func<TValue> getter)
        {
            if (this.TryGetCachedKey(subjectId, cacheKey, out TValue cached))
            {
                return cached;
            }

            var value = getter();

            this.AddKey(subjectId, cacheKey, value);

            return value;
        }
    }
}
