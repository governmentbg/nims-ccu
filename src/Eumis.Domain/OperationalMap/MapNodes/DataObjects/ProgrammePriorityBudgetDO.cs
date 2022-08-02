using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.OperationalMap.MapNodes.DataObjects
{
    public class ProgrammePriorityBudgetDO
    {
        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? EuReservedAmount { get; set; }

        public decimal? BgReservedAmount { get; set; }

        public decimal? NextThreeWithAdvances { get; set; }

        public decimal? NextThreeWithoutAdvances { get; set; }

        public bool IsActive { get; set; }
    }
}
