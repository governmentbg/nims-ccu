using System.Collections.Generic;

namespace Eumis.Domain.HistoricContracts.DataObjects
{
    public class HistoricContractProcurementPlanDO
    {
        public HistoricContractProcurementPlanDO()
        {
            this.PositionNames = new List<HistoricContractProcurementPlanPositionDO>();
        }

        public string ProcurementPlanName { get; set; }

        public decimal? Amount { get; set; }

        public IList<HistoricContractProcurementPlanPositionDO> PositionNames { get; set; }
    }
}
