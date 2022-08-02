using System;
using System.Runtime.Caching;

namespace Eumis.Public.Common.Helpers
{
    public class InMemoryCache : ICacheService
    {
        public static readonly string DefaultKey = "_default";

        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback)
            where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddYears(1));
            }

            return item;
        }

        public void Update<T>(string cacheKey, T newValue)
            where T : class
        {
            if (MemoryCache.Default.Contains(cacheKey))
            {
                MemoryCache.Default.Remove(cacheKey);
                MemoryCache.Default.Add(cacheKey, newValue, DateTime.Now.AddYears(1));
            }
        }
    }
}
