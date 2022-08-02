using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.Blob.Host.Auth
{
    internal abstract class BlobAccessContext : IBlobAccessContext
    {
        public abstract bool IsUploader { get; }

        public abstract bool CanAccess(Guid blobKey);

        public void AssertCanAccess(Guid blobKey)
        {
            if (!this.CanAccess(blobKey))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }
    }
}
