using System;

namespace Eumis.Data.ContractReportFinancialCSDs.PortalViewObjects
{
    public class ContractReportFinancialCSDBudgetItemPVO
    {
        public Guid ContractBudgetLevel3AmountGid { get; set; }

        public decimal? ApprovedCumulativeEuAmount { get; set; }

        public decimal? ApprovedCumulativeBgAmount { get; set; }

        public decimal? ApprovedCumulativeBfpTotalAmount { get; set; }

        public decimal? ApprovedCumulativeSelfAmount { get; set; }

        public decimal? ApprovedCumulativeTotalAmount { get; set; }
    }
}
