using Eumis.Common.Json;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;
using Newtonsoft.Json;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionInfoDO
    {
        public EvalSessionInfoDO()
        {
        }

        public EvalSessionInfoDO(EvalSession evalSession, string procedureName, bool procedureHasMonitorstatInquiries, EvalSessionActionsVO actions)
        {
            this.EvalSessionId = evalSession.EvalSessionId;
            this.ProcedureId = evalSession.ProcedureId;
            this.EvalSessionStatus = evalSession.EvalSessionStatus;
            this.EvalSessionStatusName = evalSession.EvalSessionStatus;
            this.ProcedureName = procedureName;
            this.SessionNum = evalSession.SessionNum;
            this.Actions = actions;
            this.ProcedureHasMonitorstatInquiries = procedureHasMonitorstatInquiries;

            this.Version = evalSession.Version;
        }

        public int EvalSessionId { get; set; }

        public int ProcedureId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionStatus EvalSessionStatus { get; set; }

        public EvalSessionStatus EvalSessionStatusName { get; set; }

        public string ProcedureName { get; set; }

        public string SessionNum { get; set; }

        public EvalSessionActionsVO Actions { get; set; }

        public bool ProcedureHasMonitorstatInquiries { get; set; }

        public byte[] Version { get; set; }
    }
}
