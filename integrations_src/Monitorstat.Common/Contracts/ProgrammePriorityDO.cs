using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitorstat.Common.Contracts
{
    public class ProgrammePriorityDO
    {
        public Guid ProgrammeIdentifier { get; set; }

        public string ProgrammeCode { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string SpecificTargets { get; set; }

        public string Definition { get; set; }
    }
}
