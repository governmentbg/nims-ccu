using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.Authorization
{
    public class ClaimKey
    {
        public ClaimKey(string claim, params string[] claimArgs)
        {
            this.Key = this.CreateStringKey(claim, claimArgs);
        }

        internal string Key { get; private set; }

        private string CreateStringKey(string claim, params string[] claimArgs)
        {
            return claimArgs.Aggregate(claim, (s1, s2) => s1 + "#" + s2);
        }
    }
}
