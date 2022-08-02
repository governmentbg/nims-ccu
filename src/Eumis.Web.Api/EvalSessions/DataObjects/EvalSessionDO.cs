using Eumis.Domain.EvalSessions;
using System;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionDO
    {
        public EvalSessionDO()
        {
            this.EvalSessionStatus = Eumis.Domain.EvalSessions.EvalSessionStatus.Draft;
            this.SessionDate = DateTime.Now;
            this.EvalSessionType = Eumis.Domain.EvalSessions.EvalSessionType.ProjectEvaluation;
        }

        public EvalSessionDO(EvalSession evalSession)
        {
            this.EvalSessionId = evalSession.EvalSessionId;
            this.ProcedureId = evalSession.ProcedureId;
            this.EvalSessionStatus = evalSession.EvalSessionStatus;
            this.EvalSessionType = evalSession.EvalSessionType;
            this.SessionNum = evalSession.SessionNum;
            this.SessionDate = evalSession.SessionDate;
            this.OrderNum = evalSession.OrderNum;
            this.OrderDate = evalSession.OrderDate;

            this.Version = evalSession.Version;
        }

        public int? EvalSessionId { get; set; }

        public int? ProcedureId { get; set; }

        public EvalSessionStatus? EvalSessionStatus { get; set; }

        public EvalSessionType? EvalSessionType { get; set; }

        public string SessionNum { get; set; }

        public DateTime? SessionDate { get; set; }

        public string OrderNum { get; set; }

        public DateTime? OrderDate { get; set; }

        public byte[] Version { get; set; }
    }
}
