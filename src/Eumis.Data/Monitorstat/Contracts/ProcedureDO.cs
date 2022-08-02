using System;

namespace Eumis.Data.Monitorstat.Contracts
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
