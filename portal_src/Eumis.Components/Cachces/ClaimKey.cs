using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Components.Caches
{
    public class CacheKey
    {
        public CacheKey(string key, params string[] keyArgs)
        {
            this.Key = this.CreateStringKey(key, keyArgs);
        }

        internal string Key { get; private set; }

        private string CreateStringKey(string key, params string[] keyArgs)
        {
            return keyArgs.Aggregate(key, (s1, s2) => s1 + "#" + s2);
        }
    }
}
