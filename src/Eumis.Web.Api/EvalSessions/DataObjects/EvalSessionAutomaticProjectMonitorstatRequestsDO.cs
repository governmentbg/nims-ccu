using System.Collections.Generic;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionAutomaticProjectMonitorstatRequestsDO
    {
        public IList<int> ProjectIds { get; set; }

        public int ProcedureMonitorstatRequestId { get; set; }

        public int? ProcedureApplicationDocId { get; set; }

        public int? ProgrammeDeclarationId { get; set; }
    }
}
