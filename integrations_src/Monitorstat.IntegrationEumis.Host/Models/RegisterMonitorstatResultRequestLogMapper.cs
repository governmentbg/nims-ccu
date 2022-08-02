using Monitorstat.Common.MonitorstatService;

namespace Monitorstat.IntegrationEumis.Host.Models
{
    public class RegisterMonitorstatResultRequestLogMapper
    {
        public RegisterMonitorstatResultRequestLogMapper(RegisterMonitorstatResultRequest request)
        {
            this.ProcedureIdentifier = request.ProcedureIdentifier;
            this.SubjectIdentifier = request.SubjectIdentifier;
            this.SubjectIdentifierType = request.SubjectIdentifierType;
            this.FileName = request.File?.Name;
            this.FileSize = request.File?.Size;
        }

        public string ProcedureIdentifier { get; set; }

        public string SubjectIdentifier { get; set; }

        public IdentifierType SubjectIdentifierType { get; set; }

        public string FileName { get; set; }

        public int? FileSize { get; set; }
    }
}
