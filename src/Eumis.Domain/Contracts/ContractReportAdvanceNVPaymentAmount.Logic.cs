using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportAdvanceNVPaymentAmount
    {
        public void UpdateAttributes(
            ContractReportAdvanceNVPaymentAmountApproval? approval,
            string notes,
            decimal? euAmount,
            decimal? bgAmount,
            decimal? bfpTotalAmount)
        {
            this.Approval = approval;
            this.Notes = notes;

            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
            this.BfpTotalAmount = bfpTotalAmount;

            this.ModifyDate = DateTime.Now;
        }
    }
}
