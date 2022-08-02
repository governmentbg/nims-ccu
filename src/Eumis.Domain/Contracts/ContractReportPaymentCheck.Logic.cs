using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportPaymentCheck
    {
        public void UpdateAttributes(
            ContractReportPaymentCheckApproval? approval,
            Guid? blobKey,
            DateTime? checkedDate)
        {
            this.Approval = approval;
            this.BlobKey = blobKey;
            this.CheckedDate = checkedDate;

            this.ModifyDate = DateTime.Now;
        }

        public ContractReportPaymentCheckAmount FindContractReportPaymentCheckAmount(int contractReportPaymentCheckAmountId)
        {
            var crpca = this.ContractReportPaymentCheckAmounts.Where(e => e.ContractReportPaymentCheckAmountId == contractReportPaymentCheckAmountId).SingleOrDefault();

            if (crpca == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ContractReportPaymentCheckAmount with id " + contractReportPaymentCheckAmountId);
            }

            return crpca;
        }

        public void AddContractReportPaymentCheckAmount(
            int programmePriorityId,
            decimal? approvedEuAmount,
            decimal? approvedBgAmount,
            decimal? approvedBfpTotalAmount,
            decimal? approvedCrossAmount,
            decimal? approvedSelfAmount,
            decimal? approvedTotalAmount,
            decimal? paidEuAmount,
            decimal? paidBgAmount,
            decimal? paidBfpTotalAmount,
            decimal? paidCrossAmount)
        {
            ContractReportPaymentCheckAmount crpca = new ContractReportPaymentCheckAmount(
                this.ContractReportPaymentCheckId,
                this.ContractReportPaymentId,
                this.ContractReportId,
                this.ContractId,
                programmePriorityId,
                approvedEuAmount,
                approvedBgAmount,
                approvedBfpTotalAmount,
                approvedCrossAmount,
                approvedSelfAmount,
                approvedTotalAmount,
                paidEuAmount,
                paidBgAmount,
                paidBfpTotalAmount,
                paidCrossAmount);

            this.ContractReportPaymentCheckAmounts.Add(crpca);

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateContractReportPaymentCheckAmounts(IList<ContractReportPaymentCheckAmountDO> amounts)
        {
            foreach (var amount in amounts)
            {
                var crpca = this.FindContractReportPaymentCheckAmount(amount.ContractReportPaymentCheckAmountId);

                crpca.SetAttributes(
                    amount.PaidEuAmount,
                    amount.PaidBgAmount,
                    amount.PaidBfpTotalAmount,
                    amount.PaidCrossAmount);
            }

            this.ModifyDate = DateTime.Now;
        }
    }
}
