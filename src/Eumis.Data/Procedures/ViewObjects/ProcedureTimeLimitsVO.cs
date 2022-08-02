using System;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureTimeLimitsVO
    {
        public int ProcedureTimeLimitId { get; set; }

        public DateTime EndDate { get; set; }

        public byte[] Version { get; set; }

        public string Notes { get; set; }
    }
}
