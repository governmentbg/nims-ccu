using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitorstat.Common.Contracts
{
    public class ProgrammeDO
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string Description { get; set; }
    }
}
