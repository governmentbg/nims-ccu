using System;
using System.Linq;
using System.Security;

namespace Eumis.Blob.Host.Auth
{
    internal class AuthenticatedBlobAccessContext : BlobAccessContext
    {
        private Guid allowedBlobKey;
        private bool isUploader;

        public AuthenticatedBlobAccessContext(Guid allowedBlobKey, bool isUploader)
        {
            this.allowedBlobKey = allowedBlobKey;
            this.isUploader = isUploader;
        }

        public override bool IsUploader => this.isUploader;

        public override bool CanAccess(Guid blobKey)
        {
            return this.allowedBlobKey == blobKey;
        }
    }
}
