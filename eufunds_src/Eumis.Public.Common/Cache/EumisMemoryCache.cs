using System;
using System.Runtime.Caching;

namespace Eumis.Public.Common.CacheProvider
{
    public static class EumisMemoryCache
    {
        private static MemoryCache cache = new MemoryCache(nameof(EumisMemoryCache));

        public static T GetItem<T>(string key, Func<T> valueFactory)
        {
            return AddOrGetExisting<T>(key, () => valueFactory());
        }

        private static T AddOrGetExisting<T>(string key, Func<T> valueFactory)
        {
            var newValue = new Lazy<T>(valueFactory);

            var oldValue = cache.AddOrGetExisting(key, newValue, new CacheItemPolicy() { AbsoluteExpiration = Configuration.AbsoluteExpiration }) as Lazy<T>;
            try
            {
                return (oldValue ?? newValue).Value;
            }
            catch
            {
                cache.Remove(key);
                throw;
            }
        }
    }
}
