using Eumis.Domain.OperationalMap.ProgrammePriorities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.AnnualAccountReports.DataObjects
{
    public class AnnualAccountReportAppendixDO
    {
        public AnnualAccountReportAppendixDO()
        {
        }

        public AnnualAccountReportAppendixDO(AnnualAccountReportAppendix appendix, ProgrammePriority programmePriority, byte[] version)
        {
            this.AnnualAccountReportAppendixId = appendix.AnnualAccountReportAppendixId;
            this.Comment = appendix.Comment;
            this.Version = version;
            this.ProgrammePriorityCode = programmePriority.Code;
            this.ProgrammePriorityName = programmePriority.Name;
        }

        public int AnnualAccountReportAppendixId { get; set; }

        public string ProgrammePriorityName { get; set; }

        public string ProgrammePriorityCode { get; set; }

        public string Comment { get; set; }

        public byte[] Version { get; set; }
    }
}
