using Monitorstat.IntegrationEumis.Host.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host.Communicators
{
    public interface IBlobCommunicator
    {
        BlobInfo UploadBlob(string fileName, Stream fileStream);

        BlobInfo UploadBlob(Monitorstat.Common.MonitorstatService.File file);
    }
}
