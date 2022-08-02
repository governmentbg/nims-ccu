using System;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
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
