using System.Collections.Generic;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.OperationalMap.Programmes.ViewObjects
{
    public class ProgrammeBudgetsWrapperVO
    {
        public string BudgetPeriod { get; set; }

        public ProgrammeBudgetsVO Budgets { get; set; }
    }
}
