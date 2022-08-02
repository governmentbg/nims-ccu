using System.Collections.Generic;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.OperationalMap.ProgrammePriorities.ViewObjects
{
    public class ProgrammePriorityBudgetsWrapperVO
    {
        public int ProgrammePriorityId { get; set; }

        public int BudgetPeriodId { get; set; }

        public string BudgetPeriod { get; set; }

        public ProgrammePriorityBudgetsVO Budgets { get; set; }
    }
}
