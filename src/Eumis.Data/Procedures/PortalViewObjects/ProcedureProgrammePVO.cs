using System.Collections.Generic;
using System.Linq;

using Eumis.Domain.Procedures.Json;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProcedureProgrammePVO
    {
        public ProcedureProgrammePVO(ProcedureProgrammeJson programme)
        {
            this.ProgrammeCode = programme.ProgrammeCode;
            this.ProgrammeName = programme.ProgrammeName;
            this.ProgrammeNameAlt = programme.ProgrammeNameAlt;

            this.ProgrammePriorities = programme.ProgrammePriorities.Select(pp => new ProgrammePriorityPVO(pp)).ToList();
            this.Indicators = programme.Indicators.Select(i => new IndicatorPVO(i)).ToList();
            this.BudgetExpenseTypes = programme.BudgetExpenseTypes.Select(et => new BudgetExpenseTypePVO(et)).ToList();
        }

        public string ProgrammeCode { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammeNameAlt { get; set; }

        public IList<ProgrammePriorityPVO> ProgrammePriorities { get; set; }

        public IList<IndicatorPVO> Indicators { get; set; }

        public IList<BudgetExpenseTypePVO> BudgetExpenseTypes { get; set; }
    }
}
