using System;
using System.Collections.Generic;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Monitorstat.Contracts
{
    public class ProcedureInquiryDO
    {
        public ProcedureInquiryDO()
        {
            this.Reports = new List<ReportDO>();
            this.Activities = new List<ActivityDO>();
        }

        public ProcedureInquiryDO(ProcedureMonitorstatRequest request)
            : this()
        {
            this.ExecutionDateTo = request.ExecutionEndDate.Value;
            this.ExecutionDateFrom = request.ExecutionStartDate.Value;
        }

        public string ProcedureCode { get; set; }

        public IList<ReportDO> Reports { get; set; }

        public IList<ActivityDO> Activities { get; set; }

        public DateTime FinishDate { get; set; }

        public DateTime ExecutionDateTo { get; set; }

        public DateTime ExecutionDateFrom { get; set; }
    }
}
