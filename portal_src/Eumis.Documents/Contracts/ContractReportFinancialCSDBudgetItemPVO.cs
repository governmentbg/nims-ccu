using System;
namespace Eumis.Documents.Contracts
{
    public class ContractReportFinancialCSDBudgetItemPVO
    {
        public decimal? ApprovedCumulativeBfpTotalAmount { get; set; }

        public decimal? ApprovedCumulativeBgAmount { get; set; }

        public decimal? ApprovedCumulativeEuAmount { get; set; }

        public decimal? ApprovedCumulativeSelfAmount { get; set; }

        public decimal? ApprovedCumulativeTotalAmount { get; set; }

        public Guid ContractBudgetLevel3AmountGid { get; set; }
    }
}