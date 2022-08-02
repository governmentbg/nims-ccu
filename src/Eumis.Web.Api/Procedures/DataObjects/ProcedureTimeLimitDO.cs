using System;
using Eumis.Common;
using Eumis.Domain.Procedures;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureTimeLimitDO
    {
        public ProcedureTimeLimitDO()
        {
        }

        public ProcedureTimeLimitDO(int procedureId, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.Version = version;
        }

        public ProcedureTimeLimitDO(ProcedureTimeLimit procedureTimeLimit, byte[] version)
        {
            var endDate = procedureTimeLimit.EndDate;
            this.EndDate = endDate.Date;
            this.EndTime = endDate.ConvertHoursToMilliseconds();

            this.ProcedureTimeLimitId = procedureTimeLimit.ProcedureTimeLimitId;
            this.ProcedureId = procedureTimeLimit.ProcedureId;
            this.Notes = procedureTimeLimit.Notes;
            this.Version = version;
        }

        public int? ProcedureTimeLimitId { get; set; }

        public int? ProcedureId { get; set; }

        public DateTime? EndDate { get; set; }

        public int? EndTime { get; set; }

        public string Notes { get; set; }

        public byte[] Version { get; set; }
    }
}
