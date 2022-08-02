using System;

namespace Eumis.ApplicationServices.Communicators
{
    public class BlobInfo
    {
        public Guid FileKey { get; set; }

        public string AccessToken { get; set; }

        public long Size { get; set; }

        public string Hash { get; set; }
    }
}
