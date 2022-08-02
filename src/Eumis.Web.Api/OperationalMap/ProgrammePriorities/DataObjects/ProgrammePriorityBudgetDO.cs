using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Web.Api.OperationalMap.Programmes.DataObjects;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.DataObjects
{
    public class ProgrammePriorityBudgetDO
    {
        public ProgrammePriorityBudgetDO()
        {
            this.BudgetAmount = new OperationalMapBudgetDO();
            this.ReservedBudgetAmount = new OperationalMapBudgetDO();
            this.NextThreeAmount = new ProgrammePriorityBudgetNextThreeDO();
            this.IsActive = false;
        }

        public ProgrammePriorityBudgetDO(MapNodeBudget programmePriorityBudget)
        {
            this.ProgrammePriorityId = programmePriorityBudget.MapNodeId;
            this.BudgetPeriodId = programmePriorityBudget.BudgetPeriodId;

            this.BudgetAmount = new OperationalMapBudgetDO(programmePriorityBudget.BgAmount);
            this.ReservedBudgetAmount = new OperationalMapBudgetDO(programmePriorityBudget.BgReservedAmount);
            this.NextThreeAmount = new ProgrammePriorityBudgetNextThreeDO(programmePriorityBudget.NextThreeWithAdvances, programmePriorityBudget.NextThreeWithoutAdvances);
            this.IsActive = true;
        }

        public int ProgrammePriorityId { get; set; }

        public int BudgetPeriodId { get; set; }

        public OperationalMapBudgetDO BudgetAmount { get; set; }

        public OperationalMapBudgetDO ReservedBudgetAmount { get; set; }

        public ProgrammePriorityBudgetNextThreeDO NextThreeAmount { get; set; }

        public bool IsActive { get; set; }
    }
}
