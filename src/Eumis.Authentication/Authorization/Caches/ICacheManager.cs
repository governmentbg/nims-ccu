using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.Authorization
{
    public interface ICacheManager
    {
        void ClearCache(ClaimsCaches cache, int subjectId);
    }
}
