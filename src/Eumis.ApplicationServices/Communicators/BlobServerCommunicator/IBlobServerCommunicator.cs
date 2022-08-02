using System;
using System.IO;

namespace Eumis.ApplicationServices.Communicators
{
    public interface IBlobServerCommunicator
    {
        string GetBlobAccessToken(Guid blobKey, bool useUploaderToken);

        Uri GetBlobUriWithAccessToken(Guid blobKey, bool useUploaderToken);

        Stream GetBlobStream(Guid blobKey, bool useUploaderToken);

        BlobInfo UploadBlob(string filename, Stream blobStream);
    }
}
