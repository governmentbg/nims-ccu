using System;
using System.Collections.Specialized;

namespace Eumis.Public.Common.Config
{
    public static class NameValueCollectionExtensions
    {
        public static string GetWithEnv(this NameValueCollection nvc, string name)
        {
            if (nvc == null)
            {
                throw new ArgumentNullException(nameof(nvc));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return nvc.Get(name).ExpandEnv();
        }
    }
}
