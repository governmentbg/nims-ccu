using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportAdvancePaymentAmount
{
    public interface IContractReportAdvancePaymentAmountService
    {
        void CreateContractReportAdvancePaymentAmounts(ContractReportPayment payment);

        void DeleteContractReportAdvancePaymentAmounts(ContractReportPayment payment);

        Eumis.Domain.Contracts.ContractReportAdvancePaymentAmount UpdateContractReportAdvancePaymentAmount(
            int contractReportAdvancePaymentAmountId,
            byte[] version,
            ContractReportAdvancePaymentAmountApproval? approval,
            string notes,
            decimal? approvedEuAmount,
            decimal? approvedBgAmount,
            decimal? approvedBfpTotalAmount);

        void ChangeContractReportAdvancePaymentAmountStatus(
            int contractReportAdvancePaymentAmountId,
            byte[] version,
            ContractReportAdvancePaymentAmountStatus status);

        IList<string> CanChangeContractReportAdvancePaymentAmountStatusToEnded(int contractReportAdvancePaymentAmountId);

        IList<string> CanChangeContractReportAdvancePaymentAmountStatusToDraft(int contractReportAdvancePaymentAmountId);
    }
}
