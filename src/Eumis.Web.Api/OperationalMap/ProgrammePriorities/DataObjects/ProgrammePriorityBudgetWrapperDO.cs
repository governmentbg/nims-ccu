using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.DataObjects
{
    public class ProgrammePriorityBudgetWrapperDO
    {
        public ProgrammePriorityBudgetWrapperDO()
        {
            this.Budgets = new ProgrammePriorityBudgetDO();
        }

        public ProgrammePriorityBudgetWrapperDO(int programmePriorityId, byte[] version)
        {
            this.ProgrammePriorityId = programmePriorityId;
            this.Version = version;

            this.Budgets = new ProgrammePriorityBudgetDO();
        }

        public ProgrammePriorityBudgetWrapperDO(
            IList<MapNodeBudget> budgets,
            byte[] version)
        {
            var firstBudget = budgets.First();

            this.ProgrammePriorityId = firstBudget.MapNodeId;
            this.BudgetPeriodId = firstBudget.BudgetPeriodId;
            this.Version = version;

            this.Budgets = new ProgrammePriorityBudgetDO();
        }

        public int ProgrammePriorityId { get; set; }

        public int? BudgetPeriodId { get; set; }

        public ProgrammePriorityBudgetDO Budgets { get; set; }

        public byte[] Version { get; set; }
    }
}
