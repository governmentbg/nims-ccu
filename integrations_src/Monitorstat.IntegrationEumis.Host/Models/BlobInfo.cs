using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host.Models
{
    public class BlobInfo
    {
        public Guid FileKey { get; set; }

        public string AccessToken { get; set; }

        public long Size { get; set; }

        public string Hash { get; set; }

        public string FileName { get; set; }
    }
}
