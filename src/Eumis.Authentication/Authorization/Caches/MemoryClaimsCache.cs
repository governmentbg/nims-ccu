using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;

namespace Eumis.Authentication.Authorization
{
    internal class MemoryClaimsCache : IClaimsCache
    {
        private ConcurrentDictionary<int, ConcurrentDictionary<string, object>> cache = new ConcurrentDictionary<int, ConcurrentDictionary<string, object>>();

        public bool TryGetCachedClaim<TValue>(int subjectId, ClaimKey claimKey, out TValue value)
        {
            if (this.cache.TryGetValue(subjectId, out var accessorClaims))
            {
                if (accessorClaims.TryGetValue(claimKey.Key, out var storedValue))
                {
                    value = (TValue)storedValue;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }

        public void AddClaim<TValue>(int subjectId, ClaimKey claimKey, TValue value)
        {
            var accessorClaims = this.cache.GetOrAdd(subjectId, (a) => new ConcurrentDictionary<string, object>());
            accessorClaims.AddOrUpdate(claimKey.Key, value, (k, v) => v);
        }

        public void ClearCache(int subjectId)
        {
            this.cache.TryRemove(subjectId, out var accessorClaims);
        }

        public int GetAllocatedMemory()
        {
            int totalMemory = this.cache.Count * sizeof(int);

            foreach (var cachedItem in this.cache)
            {
                foreach (var cachedValues in cachedItem.Value)
                {
                    totalMemory += Encoding.Unicode.GetByteCount(cachedValues.Key);
                    totalMemory += this.GetAllocatedMemoryForObject(cachedValues.Value);
                }
            }

            return totalMemory;
        }

        // working only for primitives, enums, strings and array of these types
        private int GetAllocatedMemoryForObject<T>(T o)
        {
            var type = o.GetType();
            var result = 0;

            if (type == typeof(string))
            {
                result = Encoding.Unicode.GetByteCount(o as string);
            }
            else if (type.IsEnum)
            {
                result = sizeof(int);
            }
            else if (type.IsPrimitive)
            {
                result = Marshal.SizeOf(o);
            }
            else if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var arr = o as Array;

                if (elementType.IsPrimitive)
                {
                    result = Marshal.SizeOf(elementType) * arr.Length;
                }
                else
                {
                    foreach (var el in arr)
                    {
                        result += this.GetAllocatedMemoryForObject(el);
                    }
                }
            }

            return result;
        }
    }
}
