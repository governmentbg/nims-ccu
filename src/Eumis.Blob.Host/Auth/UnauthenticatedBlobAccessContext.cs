using System;

namespace Eumis.Blob.Host.Auth
{
    internal class UnauthenticatedBlobAccessContext : BlobAccessContext
    {
        public override bool IsUploader => throw new NotSupportedException();

        public override bool CanAccess(Guid blobKey)
        {
            return false;
        }
    }
}
