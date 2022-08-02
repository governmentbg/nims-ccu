using System.Collections.Generic;

namespace Eumis.Domain.Procedures.Json
{
    public class ProcedureProgrammeJson
    {
        public ProcedureProgrammeJson()
        {
            this.ProgrammePriorities = new List<ProgrammePriorityJson>();
            this.Indicators = new List<IndicatorJson>();
            this.BudgetExpenseTypes = new List<BudgetExpenseTypeJson>();
        }

        public int ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammeNameAlt { get; set; }

        public string ProgrammeCode { get; set; }

        public IList<ProgrammePriorityJson> ProgrammePriorities { get; set; }

        public IList<IndicatorJson> Indicators { get; set; }

        public IList<BudgetExpenseTypeJson> BudgetExpenseTypes { get; set; }
    }
}
