using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Domain.Monitorstat;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;

namespace Eumis.ApplicationServices.Services.Monitorstat
{
    public interface IMonitorstatService
    {
        IList<string> CanLoadExternalSurveys(MonitorstatYear year);

        void LoadExternalSurveys(MonitorstatYear year);

        void SendProcedureMonitorstatRequest(int procedureId, int procedureMonitorstatRequestId, byte[] version);

        IList<string> CanSendProcedureMonitorstatRequest(int procedureMonitorstatRequestId);

        void SendProjectMonitorstatRequest(int projectId, int projectMonitorstatRequestId, byte[] version, int userId);

        IList<string> CanSendProjectMonitorstatRequest(int projectId, int projectMonitorstatRequestId);

        IList<string> ReceiveMonitorstatFile(string procedureCode, string uin, UinType uinType, string fileName, Guid fileKey, Guid? subjectRequestGuid);

        ProjectMonitorstatRequest CreateProjectMonitorstatRequest(int projectId, byte[] version, int procedureMonitorstatRequestId, int projectVersionXmlId, int? programmeDeclarationId, int? projectVersionXmlFileId);

        void UpdateProjectMonitorstatRequest(
            int projectId,
            byte[] version,
            int projectMonitorstatRequestId,
            int procedureMonitorstatRequestId,
            int? projectVersionXmlFileId,
            int? programmeDeclarationId);

        IList<string> SendAutomaticProjectMonitorstatRequests(
            IList<int> projectIds,
            int evalSessionId,
            int procedureMonitorstatRequestId,
            int? procedureApplicationDocId,
            int? programmeDeclarationId);
    }
}
