using Eumis.Common.Localization;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;

namespace Eumis.Data.EvalSessions.PortalViewObjects
{
    public class EvalSessionSheetData
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectNameAlt { get; set; }

        public string ProjectRegNumber { get; set; }

        public string AssessorName { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string CompanyName { get; set; }

        public string CompanyNameAlt { get; set; }

        public ProcedureEvalTableType EvalTableType { get; set; }

        public EvalSessionSheetStatus Status { get; set; }
    }
}
