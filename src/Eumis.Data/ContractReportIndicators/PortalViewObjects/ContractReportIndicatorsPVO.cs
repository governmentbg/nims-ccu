using System;

namespace Eumis.Data.ContractReportIndicators.PortalViewObjects
{
    public class ContractReportIndicatorsPVO
    {
        public Guid ContractIndicatorGid { get; set; }

        public decimal? ApprovedCumulativeAmountMen { get; set; }

        public decimal? ApprovedCumulativeAmountWomen { get; set; }

        public decimal? ApprovedCumulativeAmountTotal { get; set; }
    }
}
