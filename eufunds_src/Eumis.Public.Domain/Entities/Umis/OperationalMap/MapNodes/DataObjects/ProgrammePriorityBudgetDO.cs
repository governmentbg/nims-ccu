using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes.DataObjects
{
    public class ProgrammePriorityBudgetDO
    {
        public FinanceSource FinanceSource { get; set; }
        public decimal? EuAmount { get; set; }
        public decimal? BgAmount { get; set; }
        public decimal? EuReservedAmount { get; set; }
        public decimal? BgReservedAmount { get; set; }
        public decimal? NextThreeWithAdvances { get; set; }
        public decimal? NextThreeWithoutAdvances { get; set; }
        public bool IsActive { get; set; }
    }
}
