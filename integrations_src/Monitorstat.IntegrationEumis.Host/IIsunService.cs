using Monitorstat.IntegrationEumis.Host.Models;
using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Monitorstat.IntegrationEumis.Host
{
    [ServiceContract]
    public interface IIsunService
    {
        [OperationContract]
        void RegisterMonitorstatResult(RegisterMonitorstatResultRequest request, Guid? subjectRequestGuid);
    }
}
