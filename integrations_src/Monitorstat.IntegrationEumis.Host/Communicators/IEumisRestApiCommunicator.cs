using Monitorstat.IntegrationEumis.Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host.Communicators
{
    public interface IEumisRestApiCommunicator
    {
        Response RegisterMonitorstatDocument(RegisterEumisResultRequest request);
    }
}
