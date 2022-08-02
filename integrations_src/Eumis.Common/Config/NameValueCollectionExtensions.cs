using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common.Config
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
