using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.EvalSessions;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionReportDO
    {
        public EvalSessionReportDO()
        {
            this.Projects = new List<EvalSessionReportProjectDO>();
        }

        public EvalSessionReportDO(EvalSessionReport report)
        {
            this.EvalSessionId = report.EvalSessionId;
            this.EvalSessionReportId = report.EvalSessionReportId;
            this.Type = report.Type;
            this.RegNumber = report.RegNumber;
            this.Description = report.Description;
            this.IsDeleted = report.IsDeleted;
            this.IsDeletedNote = report.IsDeletedNote;
            this.CreateDate = report.CreateDate;
            this.ModifyDate = report.ModifyDate;
            this.Version = report.Version;
            this.Projects = report.Projects
                .Select(r => new EvalSessionReportProjectDO(r))
                .OrderBy(r => r.StandingStatus)
                .ThenBy(r => r.StandingNum)
                .ThenBy(r => r.RegNumber)
                .ToList();
        }

        public int? EvalSessionId { get; set; }

        public int? EvalSessionReportId { get; set; }

        public EvalSessionReportType? Type { get; set; }

        public string RegNumber { get; set; }

        public string Description { get; set; }

        public bool? IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public IList<EvalSessionReportProjectDO> Projects { get; set; }
    }
}
