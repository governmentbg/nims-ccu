using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitorstat.Common.Contracts
{
    public class ProcedureDO
    {
        public string ProgrammeCode { get; set; }

        public string ProgrammePriorityCode { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }
    }
}
