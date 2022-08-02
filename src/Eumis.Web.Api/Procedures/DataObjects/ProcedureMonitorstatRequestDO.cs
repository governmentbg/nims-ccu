using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureMonitorstatRequestDO
    {
        public ProcedureMonitorstatRequestDO()
        {
        }

        public ProcedureMonitorstatRequestDO(ProcedureMonitorstatRequest request)
        {
            this.ProcedureMonitorstatRequestId = request.ProcedureMonitorstatRequestId;
            this.Status = request.Status;
            this.ExecutionStartDate = request.ExecutionStartDate;
            this.ExecutionEndDate = request.ExecutionEndDate;
            this.Version = request.Version;
        }

        public int ProcedureMonitorstatRequestId { get; set; }

        public ProcedureMonitorstatRequestStatus Status { get; set; }

        public DateTime? ExecutionStartDate { get; set; }

        public DateTime? ExecutionEndDate { get; set; }

        public byte[] Version { get; set; }
    }
}
