using Eumis.Common.Json;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureMonitorstatRequestsVO
    {
        public ProcedureMonitorstatRequestsVO()
        {
        }

        public int ProcedureMonitorstatRequestId { get; set; }

        public int ProcedureId { get; set; }

        public ProcedureMonitorstatRequestStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureMonitorstatRequestStatus StatusDescr
        {
            get
            {
                return this.Status;
            }
        }

        public DateTime? ExecutionStartDate { get; set; }

        public DateTime? ExecutionEndDate { get; set; }

        public byte[] Version { get; set; }
    }
}
